using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FGScanner.Services;

public static class InventoryRealtimeClient
{
    private static readonly SemaphoreSlim ConnectionLock = new(1, 1);
    private static readonly HubConnection Connection = CreateConnection();

    public static event Action<string> InventoryChanged;

    public static bool IsConfigured => Connection != null;

    public static bool IsConnected => Connection?.State == HubConnectionState.Connected;

    private static HubConnection CreateConnection()
    {
        string hubUrl = Environment.GetEnvironmentVariable("FGIMS_SIGNALR_URL");
        if (string.IsNullOrWhiteSpace(hubUrl))
        {
            return null;
        }

        var connection = new HubConnectionBuilder()
            .WithUrl(hubUrl)
            .WithAutomaticReconnect()
            .Build();

        connection.On<string>("InventoryChanged", warehouseId =>
            InventoryChanged?.Invoke(warehouseId ?? string.Empty));

        return connection;
    }

    public static async Task StartAsync()
    {
        await TryStartAsync(CancellationToken.None);
    }

    public static async Task<bool> CheckConnectionAsync(TimeSpan timeout)
    {
        if (timeout <= TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(timeout));
        }

        using var timeoutSource = new CancellationTokenSource(timeout);
        return await TryStartAsync(timeoutSource.Token);
    }

    private static async Task<bool> TryStartAsync(CancellationToken cancellationToken)
    {
        if (Connection == null)
        {
            return false;
        }

        if (Connection.State == HubConnectionState.Connected)
        {
            return true;
        }

        if (Connection.State != HubConnectionState.Disconnected)
        {
            return false;
        }

        bool lockTaken = false;
        try
        {
            await ConnectionLock.WaitAsync(cancellationToken);
            lockTaken = true;

            if (Connection.State == HubConnectionState.Connected)
            {
                return true;
            }

            if (Connection.State != HubConnectionState.Disconnected)
            {
                return false;
            }

            await Connection.StartAsync(cancellationToken);
            return Connection.State == HubConnectionState.Connected;
        }
        catch
        {
            // The fallback timer keeps the viewer current while the hub is unavailable.
            return false;
        }
        finally
        {
            if (lockTaken)
            {
                ConnectionLock.Release();
            }
        }
    }

    public static async Task PublishInventoryChangedAsync(string warehouseId)
    {
        if (Connection == null)
        {
            return;
        }

        await StartAsync();
        if (Connection.State != HubConnectionState.Connected)
        {
            return;
        }

        try
        {
            await Connection.InvokeAsync("PublishInventoryChanged", warehouseId ?? string.Empty);
        }
        catch
        {
            // A failed notification must never affect a completed transaction.
        }
    }
}
