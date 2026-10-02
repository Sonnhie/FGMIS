using FGScanner.Models;
using FGScanner.Repositories;
using FGScanner.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace FGScanner.Tests;

public sealed class TransactionTests
{
    [Fact]
    public async Task InsertFG_WhenPartNumberDoesNotExist_RejectsTransaction()
    {
        await using var context = CreateContext();
        var service = CreateService(context);
        var scans = new List<ScannedData> { CreateScan("UNKNOWN", 100) };

        var result = await service.InsertFG(scans, "WH1", "IN", "tester");

        Assert.False(result.isSuccess);
        Assert.Equal("Partnumber 'UNKNOWN' does not exist in database.", result.Message);
        Assert.Empty(context.TransactionHistories);
    }

    [Fact]
    public async Task InsertFG_WhenQuantityIsNotPositive_RejectsTransaction()
    {
        await using var context = CreateContext();
        context.Products.Add(CreateProduct("PART-001"));
        await context.SaveChangesAsync();
        var service = CreateService(context);

        var result = await service.InsertFG(
            new List<ScannedData> { CreateScan("PART-001", 0) },
            "WH1",
            "IN",
            "tester");

        Assert.False(result.isSuccess);
        Assert.Equal("Quantity for PART-001 must be greater than zero.", result.Message);
        Assert.Empty(context.TransactionHistories);
    }

    [Fact]
    public async Task InsertReturns_WhenNoItemsAreProvided_RejectsTransaction()
    {
        await using var context = CreateContext();
        var service = CreateService(context);

        var result = await service.InsertReturns(
            new List<ScannedData>(), "WH1", "AS-001", "OUT", "tester", "FG", "9152");

        Assert.False(result.isSuccess);
        Assert.Equal("No scanned items provided.", result.Message);
        Assert.Empty(result.ValidItems);
        Assert.Empty(result.OverflowWarnings);
        Assert.Empty(context.TransactionHistories);
    }

    [Fact]
    public async Task InsertReturns_WhenPartNumberDoesNotExist_RejectsEntireTransaction()
    {
        await using var context = CreateContext();
        var service = CreateService(context);

        var result = await service.InsertReturns(
            new List<ScannedData> { CreateScan("UNKNOWN", 20) },
            "WH1", "AS-001", "OUT", "tester", "FG", "9152");

        Assert.False(result.isSuccess);
        Assert.Contains("UNKNOWN", result.Message);
        Assert.Empty(result.ValidItems);
        Assert.Empty(context.TransactionHistories);
    }

    [Fact]
    public async Task GetFilteredShipment_GroupsRowsAndSumsQuantityAndBoxes()
    {
        await using var context = CreateContext();
        context.TransactionHistories.AddRange(
            CreateHistory("SHIPID-001", "PART-001", 100, 1, new DateTime(2026, 9, 20, 8, 0, 0)),
            CreateHistory("SHIPID-001", "PART-002", 250, 2, new DateTime(2026, 9, 20, 9, 0, 0)),
            CreateHistory("OTHER-001", "PART-003", 999, 9, new DateTime(2026, 9, 20, 10, 0, 0)));
        await context.SaveChangesAsync();
        var queries = new Queries(context);

        var result = await queries.GetFilteredShipment();

        var shipment = Assert.Single(result);
        Assert.Equal("SHIPID-001", shipment.ControlNumber);
        Assert.Equal(350, shipment.Quantity);
        Assert.Equal(3, shipment.Box);
        Assert.Equal(new DateTime(2026, 9, 20, 9, 0, 0), shipment.EntryDate);
    }

    [Fact]
    public async Task GetFilteredShipment_AppliesDocumentAndInclusiveDateFilters()
    {
        await using var context = CreateContext();
        context.TransactionHistories.AddRange(
            CreateHistory("SHIPID-001", "PART-001", 100, 1, new DateTime(2026, 9, 20, 23, 59, 59)),
            CreateHistory("SHIPID-002", "PART-002", 100, 1, new DateTime(2026, 9, 21, 0, 0, 0)));
        await context.SaveChangesAsync();
        var queries = new Queries(context);

        var result = await queries.GetFilteredShipment(
            "SHIPID-001", new DateTime(2026, 9, 20), new DateTime(2026, 9, 20));

        Assert.Equal("SHIPID-001", Assert.Single(result).ControlNumber);
    }

    [Fact]
    public async Task GetShipmentItems_GroupsMatchingProductDateAndVersion()
    {
        await using var context = CreateContext();
        context.TransactionHistories.AddRange(
            CreateHistory("SHIPID-001", "PART-001", 100, 1, DateTime.Now),
            CreateHistory("SHIPID-001", "PART-001", 50, 1, DateTime.Now),
            CreateHistory("SHIPID-002", "PART-001", 999, 9, DateTime.Now));
        await context.SaveChangesAsync();
        var queries = new Queries(context);

        var result = await queries.GetShipmentItems("SHIPID-001");

        var item = Assert.Single(result);
        Assert.Equal("PART-001", item.Partnumber);
        Assert.Equal(150, item.Quantity);
        Assert.Equal(2, item.Box);
    }

    [Fact]
    public async Task CancelShipment_WhenShipmentDoesNotExist_DoesNotCreateReversal()
    {
        await using var context = CreateContext();
        var service = CreateService(context);

        var result = await service.CancelShipment("SHIPID-MISSING", "tester");

        Assert.False(result.isSuccess);
        Assert.Equal("Shipment does not exist.", result.Message);
        Assert.Empty(context.TransactionHistories);
    }

    [Fact]
    public async Task CancelShipment_WhenAlreadyCancelled_DoesNotCreateReversal()
    {
        await using var context = CreateContext();
        context.ShipmentTables.Add(CreateShipment("SHIPID-001", "Cancelled"));
        await context.SaveChangesAsync();
        var service = CreateService(context);

        var result = await service.CancelShipment("SHIPID-001", "tester");

        Assert.False(result.isSuccess);
        Assert.Equal("Shipment has already been cancelled.", result.Message);
        Assert.Empty(context.TransactionHistories);
    }

    [Fact]
    public async Task CancelReturn_WhenReturnDoesNotExist_DoesNotCreateReversal()
    {
        await using var context = CreateContext();
        var service = CreateService(context);

        var result = await service.CancelReturn("AS-MISSING", "tester");

        Assert.False(result.isSuccess);
        Assert.Equal("Warehouse Return does not exist.", result.Message);
        Assert.Empty(context.TransactionHistories);
    }

    [Fact]
    public async Task CancelReturn_WhenAlreadyCancelled_DoesNotCreateReversal()
    {
        await using var context = CreateContext();
        context.ReturnTables.Add(CreateReturn("AS-001", "cancelled"));
        await context.SaveChangesAsync();
        var service = CreateService(context);

        var result = await service.CancelReturn("AS-001", "tester");

        Assert.False(result.isSuccess);
        Assert.Equal("Warehouse Return has already been cancelled.", result.Message);
        Assert.Empty(context.TransactionHistories);
    }

    private static InventoryDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<InventoryDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        return new InventoryDbContext(options);
    }

    private static TransactionService CreateService(InventoryDbContext context) =>
        new(new Queries(context));

    private static Product CreateProduct(string partNumber) => new()
    {
        Partnumber = partNumber,
        Partname = "Test Product",
        CustomerId = "CUSTOMER",
        Pps = 100
    };

    private static ScannedData CreateScan(string partNumber, int quantity) => new()
    {
        PartNumber = partNumber,
        ProductionDate = new DateOnly(2026, 9, 20),
        ProductionVersion = "A",
        CustomerId = "CUSTOMER",
        Quantity = quantity,
        Location = "RACK-01",
        StorageLocation = "9151",
        WhId = "WH1"
    };

    private static TransactionHistory CreateHistory(
        string controlNumber,
        string partNumber,
        int quantity,
        int boxes,
        DateTime entryDate) => new()
    {
        TransactionId = Guid.NewGuid(),
        ControlNumber = controlNumber,
        Partnumber = partNumber,
        ProdDate = new DateOnly(2026, 9, 20),
        ProdVer = "A",
        CustomerId = "CUSTOMER",
        Quantity = quantity,
        Box = boxes,
        EntryDate = entryDate,
        TransactionType = "OUT",
        Location = "RACK-01",
        StorageLocation = "9151",
        WhId = "WH1",
        Remarks = "FG",
        Status = "Active",
        InCharge = "tester"
    };

    private static ShipmentTable CreateShipment(string transactionId, string status) => new()
    {
        TransactionId = transactionId,
        EntryDate = DateTime.Now,
        Status = status,
        Customer = "CUSTOMER",
        WhId = "WH1",
        ShipmentId = Guid.NewGuid()
    };

    private static ReturnTable CreateReturn(string transactionId, string status) => new()
    {
        TransactionId = transactionId,
        EntryDate = DateTime.Now,
        Status = status,
        FromLocation = "9151",
        ToLocation = "9152",
        Remarks = "FG",
        WhId = "WH1",
        ReturnId = Guid.NewGuid()
    };
}
