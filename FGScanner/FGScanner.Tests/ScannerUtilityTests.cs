using FGScanner.Util;
using Xunit;

namespace FGScanner.Tests;

public class ScannerUtilityTests
{
    private readonly ScannerUtility _scanner = new();

    [Theory]
    [InlineData("PART-REV/O10029-09-26", "PART", "REV")]
    [InlineData("PART-001-REV/O10029-09-26", "PART-001", "REV")]
    [InlineData("PART-001-ABC-REV/O10029-09-26", "PART-001-ABC", "REV")]
    [InlineData("PART-001-ABC-XYZ-REV/O10029-09-26", "PART-001-ABC-XYZ", "REV")]
    public void ProcessQRData_WithSupportedPartNumberFormat_ReturnsParsedItem(
        string qrData,
        string expectedPartNumber,
        string expectedProductionVersion)
    {
        var result = _scanner.ProcessQRData(qrData, out var item, out var error);

        Assert.True(result);
        Assert.Null(error);
        Assert.NotNull(item);
        Assert.Equal(expectedPartNumber, item.PartNumber);
        Assert.Equal(expectedProductionVersion, item.ProductionVer);
        Assert.Equal(100, item.Quantity);
        Assert.Equal(new DateTime(2026, 9, 29), item.ProductionDate);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("PART-REV")]
    [InlineData("PART-REV/EXTRA/O10029-09-26")]
    public void ProcessQRData_WithInvalidOverallFormat_ReturnsError(string? qrData)
    {
        var result = _scanner.ProcessQRData(qrData!, out var item, out var error);

        Assert.False(result);
        Assert.Null(item);
        Assert.Equal("Invalid QR Code!", error);
    }

    [Fact]
    public void ProcessQRData_WithMissingProductionVersion_ReturnsError()
    {
        var result = _scanner.ProcessQRData("PART/O10029-09-26", out var item, out var error);

        Assert.False(result);
        Assert.Null(item);
        Assert.Equal("Invalid QR Code!", error);
    }

    [Fact]
    public void ProcessQRData_WithInvalidRightHandSegments_ReturnsError()
    {
        var result = _scanner.ProcessQRData("PART-REV/O10029-09", out var item, out var error);

        Assert.False(result);
        Assert.Null(item);
        Assert.Equal("Invalid QR Code", error);
    }

    [Theory]
    [InlineData("PART-REV/X10029-09-26")]
    [InlineData("PART-REV/O1-09-26")]
    public void ProcessQRData_WithInvalidOrderFormat_ReturnsError(string qrData)
    {
        var result = _scanner.ProcessQRData(qrData, out var item, out var error);

        Assert.False(result);
        Assert.Null(item);
        Assert.Equal("Invalid order format!", error);
    }

    [Fact]
    public void ProcessQRData_WithInvalidDate_ReturnsError()
    {
        var result = _scanner.ProcessQRData("PART-REV/O10031-02-26", out var item, out var error);

        Assert.False(result);
        Assert.Null(item);
        Assert.Equal("Invalid date format!", error);
    }

    [Fact]
    public void ProcessQRData_WithNonNumericQuantity_ReturnsError()
    {
        var result = _scanner.ProcessQRData("PART-REV/OABC29-09-26", out var item, out var error);

        Assert.False(result);
        Assert.Null(item);
        Assert.Equal("Invalid quantity format!", error);
    }
}
