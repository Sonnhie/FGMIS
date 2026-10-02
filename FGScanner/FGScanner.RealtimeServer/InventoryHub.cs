using Microsoft.AspNetCore.SignalR;

namespace FGScanner.RealtimeServer;

public sealed class InventoryHub : Hub
{
    public Task PublishInventoryChanged(string warehouseId) =>
        Clients.All.SendAsync("InventoryChanged", warehouseId ?? string.Empty);
}
