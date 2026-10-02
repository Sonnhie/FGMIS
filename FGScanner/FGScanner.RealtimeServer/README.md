# FGIMS Realtime Server

Run one copy of this service on a computer reachable by all FGIMS clients:

```powershell
dotnet run --project FGScanner.RealtimeServer.csproj -c Release
```

The service listens on TCP port `5055` and exposes the SignalR hub at
`http://SERVER-IP:5055/inventoryHub`.

On every FGIMS client, add this setting to `C:\FGIMS\dbconfig.env`:

```text
FGIMS_SIGNALR_URL=http://SERVER-IP:5055/inventoryHub
```

Replace `SERVER-IP` with the address of the computer running this service. If
the setting is omitted, SignalR stays disabled and the viewer safely uses its
two-minute fallback refresh. For a server running on the same computer, use:

```text
FGIMS_SIGNALR_URL=http://localhost:5055/inventoryHub
```
