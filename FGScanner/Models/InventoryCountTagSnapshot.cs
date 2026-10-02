namespace FGScanner.Models;

public class InventoryCountTagSnapshot
{
    public int Id { get; set; }
    public int BatchId { get; set; }
    public int SequenceNumber { get; set; }
    public string TagNumber { get; set; } = string.Empty;
    public string ErpLocation { get; set; } = string.Empty;
    public string RackLocation { get; set; } = string.Empty;
    public string LocationDescription { get; set; } = string.Empty;
    public string PartNumber { get; set; } = string.Empty;
    public string ProductionVersion { get; set; } = string.Empty;
    public string MaterialName { get; set; } = string.Empty;
    public string Unit { get; set; } = "PC";
    public int SystemBoxes { get; set; }
    public int SystemQuantity { get; set; }

    public virtual InventoryCountBatch Batch { get; set; }
}
