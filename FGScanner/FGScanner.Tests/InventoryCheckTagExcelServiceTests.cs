using FGScanner.DTOs;
using FGScanner.Services;
using OfficeOpenXml;
using Xunit;

namespace FGScanner.Tests;

public class InventoryCheckTagExcelServiceTests
{
    [Fact]
    public async Task ExportAsync_CreatesAuditTableWithPhysicalCountAndVarianceColumns()
    {
        string outputPath = Path.Combine(
            Path.GetTempPath(),
            $"check-tags-{Guid.NewGuid():N}.xlsx");
        var tags = new List<InventoryCheckTagData>
        {
            CreateTag("26091001001", "PART-001", 200),
            CreateTag("26091001002", "PART-002", 150)
        };

        try
        {
            var service = new InventoryCheckTagExcelService();
            var result = await service.ExportAsync(tags, outputPath);

            Assert.True(result.isSuccess, result.Message);
            Assert.True(File.Exists(outputPath));

            ExcelPackage.License.SetNonCommercialPersonal("NIDEC");
            using var package = new ExcelPackage(new FileInfo(outputPath));
            ExcelWorksheet sheet = package.Workbook.Worksheets["Inventory Count Tags"];

            Assert.NotNull(sheet);
            Assert.Equal("INVENTORY COUNT TAG EXPORT", sheet.Cells[1, 1].Text);
            Assert.Equal("Physical Count", sheet.Cells[5, 15].Text);
            Assert.Equal("Variance", sheet.Cells[5, 16].Text);
            Assert.Equal("26091001001", sheet.Cells[6, 1].Text);
            Assert.Equal("PART-001", sheet.Cells[6, 8].Text);
            Assert.Equal("A", sheet.Cells[6, 10].Text);
            Assert.Equal(200, sheet.Cells[6, 14].GetValue<int>());
            Assert.Null(sheet.Cells[6, 15].Value);
            Assert.Equal("IF(O6=\"\",\"\",O6-N6)", sheet.Cells[6, 16].Formula);
            Assert.Single(sheet.Tables);
        }
        finally
        {
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
        }
    }

    private static InventoryCheckTagData CreateTag(
        string tagNumber,
        string partNumber,
        int quantity) => new()
    {
        TagNumber = tagNumber,
        BatchCode = "CT-20260930-WH1-B001-R01",
        CutoffDate = new DateOnly(2026, 9, 30),
        WarehouseId = "WH1",
        ErpLocation = "9151",
        RackLocation = "RACK-A",
        LocationDescription = "Exact PPS",
        PartNumber = partNumber,
        ProductionVersion = "A",
        MaterialName = "Finished Part",
        Unit = "PC",
        SystemBoxes = 2,
        SystemQuantity = quantity,
        GeneratedBy = "tester"
    };
}
