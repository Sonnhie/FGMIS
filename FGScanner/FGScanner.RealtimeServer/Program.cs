using FGScanner.RealtimeServer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var app = builder.Build();
app.MapGet("/", () => "FGIMS realtime server is running.");
app.MapHub<InventoryHub>("/inventoryHub");
app.Run("http://0.0.0.0:5055");
