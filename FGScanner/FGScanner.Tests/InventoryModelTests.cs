using FGScanner.Models;
using Xunit;

namespace FGScanner.Tests;

public class InventoryModelTests
{
    [Fact]
    public void InventoryRow_TotalQty_DefaultsToBoxesTimesQuantity()
    {
        var row = new InventoryRow
        {
            Boxes = 4,
            Quantity = 25
        };

        Assert.Equal(100, row.TotalQty);
    }

    [Fact]
    public void InventoryRow_TotalQty_UsesCalculatedBppsQuantityWhenProvided()
    {
        var row = new InventoryRow
        {
            Boxes = 4,
            Quantity = 25,
            CalculatedTotalQuantity = 83
        };

        Assert.Equal(83, row.TotalQty);
    }

    [Theory]
    [InlineData(0, 50, 0)]
    [InlineData(1, 50, 1)]
    [InlineData(51, 10, 6)]
    [InlineData(100, 10, 10)]
    public void PagedResult_TotalPages_UsesCeilingDivision(
        int totalCount,
        int pageSize,
        int expectedPages)
    {
        var result = new PagedResult<object>
        {
            TotalCount = totalCount,
            PageSize = pageSize
        };

        Assert.Equal(expectedPages, result.TotalPages);
    }
}
