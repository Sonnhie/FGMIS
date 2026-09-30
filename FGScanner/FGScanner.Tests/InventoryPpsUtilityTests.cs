using FGScanner.Util;
using Xunit;

namespace FGScanner.Tests;

public class InventoryPpsUtilityTests
{
    [Fact]
    public void GetPpsType_BppsRemarkOverridesExactQuantity()
    {
        string result = InventoryPpsUtility.GetPpsType(100, 1, 100, "bpps");

        Assert.Equal(InventoryPpsUtility.Bpps, result);
    }

    [Theory]
    [InlineData(100, 1, 100)]
    [InlineData(200, 2, 100)]
    public void GetPpsType_ExactBoxQuantity_ReturnsExactPps(int quantity, int boxes, int productPps)
    {
        string result = InventoryPpsUtility.GetPpsType(quantity, boxes, productPps, "FG");

        Assert.Equal(InventoryPpsUtility.ExactPps, result);
    }

    [Theory]
    [InlineData(80, 1, 100)]
    [InlineData(190, 2, 100)]
    public void GetPpsType_NonExactBoxQuantity_ReturnsBpps(int quantity, int boxes, int productPps)
    {
        string result = InventoryPpsUtility.GetPpsType(quantity, boxes, productPps, "FG");

        Assert.Equal(InventoryPpsUtility.Bpps, result);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(100, 0)]
    public void GetPpsType_WithoutValidBoxOrProductPps_ReturnsEmptyWhenNoBoxes(int productPps, int boxes)
    {
        string result = InventoryPpsUtility.GetPpsType(100, boxes, productPps, "");

        Assert.Equal(boxes > 0 ? InventoryPpsUtility.Bpps : string.Empty, result);
    }

    [Theory]
    [InlineData("BPPS")]
    [InlineData("Finished Goods")]
    [InlineData("Shipment")]
    [InlineData("Warehouse Return")]
    [InlineData("shipment")]
    public void GetLedgerBoxQuantity_ScanCategory_CountsOneScannedItem(string category)
    {
        int result = InventoryPpsUtility.GetLedgerBoxQuantity(category, 12);

        Assert.Equal(1, result);
    }

    [Theory]
    [InlineData("Transfer", 4, 4)]
    [InlineData("Stock Adjustment", 2, 2)]
    [InlineData("Transfer", -2, 0)]
    [InlineData("Transfer", null, 0)]
    public void GetLedgerBoxQuantity_NonScanCategory_UsesNonNegativeStoredValue(
        string category,
        int? storedBoxes,
        int expected)
    {
        int result = InventoryPpsUtility.GetLedgerBoxQuantity(category, storedBoxes);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetGroupedPpsType_WithBothTypes_ReturnsMixed()
    {
        string result = InventoryPpsUtility.GetGroupedPpsType(new[] { "BPPS", "Exact PPS" });

        Assert.Equal(InventoryPpsUtility.Mixed, result);
    }

    [Fact]
    public void GetGroupedPpsType_WithCaseInsensitiveDuplicates_ReturnsSingleType()
    {
        string result = InventoryPpsUtility.GetGroupedPpsType(new[] { "BPPS", "bpps", "" });

        Assert.Equal(InventoryPpsUtility.Bpps, result);
    }

    [Fact]
    public void GetGroupedPpsType_WithNoValues_ReturnsEmpty()
    {
        Assert.Equal(string.Empty, InventoryPpsUtility.GetGroupedPpsType(null!));
        Assert.Equal(string.Empty, InventoryPpsUtility.GetGroupedPpsType(Array.Empty<string>()));
    }

    [Fact]
    public void CalculateDisplayedPps_ExactPps_UsesProductMasterValue()
    {
        decimal result = InventoryPpsUtility.CalculateDisplayedPps("Exact PPS", 180, 2, 90);

        Assert.Equal(90m, result);
    }

    [Fact]
    public void CalculateDisplayedPps_Bpps_UsesActualAverageRoundedToTwoPlaces()
    {
        decimal result = InventoryPpsUtility.CalculateDisplayedPps("BPPS", 100, 3, 90);

        Assert.Equal(33.33m, result);
    }

    [Fact]
    public void CalculateDisplayedPps_WithoutBoxes_UsesActualQuantity()
    {
        decimal result = InventoryPpsUtility.CalculateDisplayedPps("BPPS", 75, 0, 100);

        Assert.Equal(75m, result);
    }
}
