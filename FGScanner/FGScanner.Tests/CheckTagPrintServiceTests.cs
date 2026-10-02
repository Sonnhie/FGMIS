using FGScanner.DTOs;
using FGScanner.Services;
using System.Drawing;
using System.Drawing.Printing;
using Xunit;

namespace FGScanner.Tests;

public class CheckTagPrintServiceTests
{
    [Fact]
    public void PrintPage_PaginatesSixTagsPerA4Page()
    {
        var tags = Enumerable.Range(1, 7)
            .Select(index => new InventoryCheckTagData
            {
                TagNumber = index.ToString("D6"),
                CutoffDate = new DateOnly(2026, 9, 30),
                ErpLocation = "9151",
                RackLocation = "RACK-A",
                LocationDescription = "Exact PPS",
                PartNumber = $"PART-{index:D3}",
                MaterialName = "Test Material"
            })
            .ToList();

        using var bitmap = new Bitmap(827, 1169);
        using var graphics = Graphics.FromImage(bitmap);
        var pageSettings = new PageSettings
        {
            PaperSize = new PaperSize("A4", 827, 1169),
            Margins = new Margins(45, 45, 40, 40)
        };
        var marginBounds = new Rectangle(45, 40, 737, 1089);
        var pageBounds = new Rectangle(0, 0, 827, 1169);
        var service = new CheckTagPrintService();

        var firstPage = new PrintPageEventArgs(graphics, marginBounds, pageBounds, pageSettings);
        service.PrintPage(tags, firstPage);
        Assert.True(firstPage.HasMorePages);

        var secondPage = new PrintPageEventArgs(graphics, marginBounds, pageBounds, pageSettings);
        service.PrintPage(tags, secondPage);
        Assert.False(secondPage.HasMorePages);
    }
}
