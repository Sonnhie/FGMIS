using FGScanner.Models;
using FGScanner.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace FGScanner.Tests;

public class MonthEndCheckTagServiceTests
{
    [Fact]
    public async Task GenerateAsync_UsesBalanceAtCutoffAndDoesNotChangeTransactions()
    {
        await using var context = CreateContext();
        context.Products.Add(CreateProduct("PART-001", "Finished Part", 100));
        context.TransactionHistories.AddRange(
            CreateHistory("IN", 300, 3, new DateTime(2026, 9, 10), "WH1", "RACK-A", "9151"),
            CreateHistory("OUT", 100, 1, new DateTime(2026, 9, 25), "WH1", "RACK-A", "9151"),
            CreateHistory("OUT", 100, 1, new DateTime(2026, 10, 1), "WH1", "RACK-A", "9151"),
            CreateHistory("IN", 500, 5, new DateTime(2026, 9, 15), "WH2", "RACK-A", "9151"));
        await context.SaveChangesAsync();
        int transactionCount = await context.TransactionHistories.CountAsync();

        var service = new MonthEndCheckTagService(context);
        var tags = await service.GenerateAsync(
            new DateOnly(2026, 9, 30), "WH1", "", "", false, "tester");

        var tag = Assert.Single(tags);
        Assert.Equal(200, tag.SystemQuantity);
        Assert.Equal(2, tag.SystemBoxes);
        Assert.Equal("PART-001", tag.MaterialCode);
        Assert.Equal("Finished Part", tag.MaterialName);
        Assert.Equal("26091001", tag.TagNumber);
        Assert.False(tag.PrefillSystemCount);
        Assert.Equal(transactionCount, await context.TransactionHistories.CountAsync());
        Assert.False(context.ChangeTracker.HasChanges());
    }

    [Fact]
    public async Task GenerateAsync_AppliesOptionalRackAndPartFiltersAndOmitsZeroBalance()
    {
        await using var context = CreateContext();
        context.Products.AddRange(
            CreateProduct("PART-000", "Earlier Part", 25),
            CreateProduct("PART-001", "First Part", 100),
            CreateProduct("PART-002", "Second Part", 50));
        context.TransactionHistories.AddRange(
            CreateHistory("IN", 25, 1, new DateTime(2026, 9, 1), "WH1", "RACK-A", "9151", "PART-000"),
            CreateHistory("IN", 100, 1, new DateTime(2026, 9, 1), "WH1", "RACK-A", "9151", "PART-001"),
            CreateHistory("OUT", 100, 1, new DateTime(2026, 9, 2), "WH1", "RACK-A", "9151", "PART-001"),
            CreateHistory("IN", 150, 3, new DateTime(2026, 9, 3), "WH1", "RACK-B", "9152", "PART-002"),
            CreateHistory("IN", 50, 1, new DateTime(2026, 9, 3), "WH1", "RACK-B-OVERFLOW", "9152", "PART-002"));
        await context.SaveChangesAsync();

        var service = new MonthEndCheckTagService(context);
        var tags = await service.GenerateAsync(
            new DateOnly(2026, 9, 30), "WH1", "rack-b", "002", true, "tester",
            exactRackMatch: true);

        var tag = Assert.Single(tags);
        Assert.Equal("PART-002", tag.PartNumber);
        Assert.Equal("RACK-B", tag.RackLocation);
        Assert.Equal(150, tag.SystemQuantity);
        Assert.Equal("26091002", tag.TagNumber);
        Assert.True(tag.PrefillSystemCount);
    }

    [Fact]
    public async Task GenerateAsync_TreatsScanBasedMovementsAsOneBoxLikeStockList()
    {
        await using var context = CreateContext();
        context.Products.Add(CreateProduct("PART-001", "Finished Part", 100));
        var incoming = CreateHistory("IN", 80, 12, new DateTime(2026, 9, 1), "WH1", "RACK-A", "9151");
        incoming.Remarks = "FG";
        context.TransactionHistories.Add(incoming);
        await context.SaveChangesAsync();

        var service = new MonthEndCheckTagService(context);
        var tags = await service.GenerateAsync(
            new DateOnly(2026, 9, 30), "WH1", "", "", false, "tester");

        var tag = Assert.Single(tags);
        Assert.Equal(1, tag.SystemBoxes);
        Assert.Equal("BPPS", tag.LocationDescription);
    }

    [Fact]
    public async Task Snapshot_ReprintKeepsOriginalTagAndQuantityAfterBackdatedStockChange()
    {
        await using var context = CreateContext();
        context.Products.Add(CreateProduct("PART-001", "Finished Part", 100));
        context.TransactionHistories.Add(
            CreateHistory("IN", 200, 2, new DateTime(2026, 9, 10), "WH1", "RACK-A", "9151"));
        await context.SaveChangesAsync();
        var service = new MonthEndCheckTagService(context);

        var firstBatch = await service.CreateSnapshotAsync(
            new DateOnly(2026, 9, 30), "WH1", false, "", false, "counter-one");
        var firstTag = Assert.Single(firstBatch.Tags);

        context.TransactionHistories.Add(
            CreateHistory("OUT", 100, 1, new DateTime(2026, 9, 20), "WH1", "RACK-A", "9151"));
        await context.SaveChangesAsync();

        var secondBatch = await service.CreateSnapshotAsync(
            new DateOnly(2026, 9, 30), "WH1", false, "", false, "counter-two");
        var reprintedFirstBatch = await service.LoadBatchAsync(firstBatch.BatchId);
        var savedFirstTag = Assert.Single(reprintedFirstBatch.Tags);
        var secondTag = Assert.Single(secondBatch.Tags);

        Assert.Equal(1, firstBatch.Revision);
        Assert.Equal(2, secondBatch.Revision);
        Assert.Equal(200, savedFirstTag.SystemQuantity);
        Assert.Equal(firstTag.TagNumber, savedFirstTag.TagNumber);
        Assert.Equal(firstBatch.BatchCode, savedFirstTag.BatchCode);
        Assert.Equal(100, secondTag.SystemQuantity);
        Assert.Equal("260910001", firstTag.TagNumber);
        Assert.Equal("260910002", secondTag.TagNumber);
        Assert.Equal(9, firstTag.TagNumber.Length);
        Assert.Equal(9, secondTag.TagNumber.Length);
        Assert.NotEqual(firstTag.TagNumber, secondTag.TagNumber);
        Assert.Equal(2, await context.InventoryCountBatches.CountAsync());
        Assert.Equal(2, await context.InventoryCountTagSnapshots.CountAsync());
    }

    [Fact]
    public async Task FindLatestBatch_ReturnsNewestRevisionForExactScope()
    {
        await using var context = CreateContext();
        context.Products.Add(CreateProduct("PART-001", "Finished Part", 100));
        context.TransactionHistories.Add(
            CreateHistory("IN", 100, 1, new DateTime(2026, 9, 10), "WH1", "RACK-A", "9151"));
        await context.SaveChangesAsync();
        var service = new MonthEndCheckTagService(context);

        await service.CreateSnapshotAsync(
            new DateOnly(2026, 9, 30), "WH1", true, "RACK-A", false, "first");
        var second = await service.CreateSnapshotAsync(
            new DateOnly(2026, 9, 30), "WH1", true, "RACK-A", false, "second");

        var latest = await service.FindLatestBatchAsync(
            new DateOnly(2026, 9, 30), "WH1", true, "rack-a");

        Assert.NotNull(latest);
        Assert.Equal(second.BatchId, latest.BatchId);
        Assert.Equal(2, latest.Revision);
        Assert.Equal("Rack: RACK-A", latest.Scope);
    }

    private static InventoryDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<InventoryDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        return new InventoryDbContext(options);
    }

    private static Product CreateProduct(string partNumber, string partName, int pps) => new()
    {
        Partnumber = partNumber,
        Partname = partName,
        CustomerId = "CUSTOMER",
        Pps = pps
    };

    private static TransactionHistory CreateHistory(
        string transactionType,
        int quantity,
        int boxes,
        DateTime entryDate,
        string warehouse,
        string rack,
        string erpLocation,
        string partNumber = "PART-001") => new()
    {
        TransactionId = Guid.NewGuid(),
        ControlNumber = Guid.NewGuid().ToString("N"),
        Partnumber = partNumber,
        ProdDate = new DateOnly(2026, 9, 1),
        ProdVer = "",
        CustomerId = "CUSTOMER",
        Quantity = quantity,
        Box = boxes,
        EntryDate = entryDate,
        TransactionType = transactionType,
        Location = rack,
        StorageLocation = erpLocation,
        WhId = warehouse,
        Remarks = "Manual",
        Status = "Active",
        InCharge = "tester"
    };
}
