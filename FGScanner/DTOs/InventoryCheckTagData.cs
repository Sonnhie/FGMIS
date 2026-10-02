using System;

namespace FGScanner.DTOs
{
    public sealed class InventoryCheckTagData
    {
        public string TagNumber { get; set; } = string.Empty;
        public string BatchCode { get; set; } = string.Empty;
        public DateOnly CutoffDate { get; set; }
        public string WarehouseId { get; set; } = string.Empty;
        public string ErpLocation { get; set; } = string.Empty;
        public string RackLocation { get; set; } = string.Empty;
        public string LocationDescription { get; set; } = string.Empty;
        public string PartNumber { get; set; } = string.Empty;
        public string ProductionVersion { get; set; } = string.Empty;
        public string MaterialName { get; set; } = string.Empty;
        public string Unit { get; set; } = "PC";
        public int ClassCode { get; set; } = 1;
        public int SystemBoxes { get; set; }
        public int SystemQuantity { get; set; }
        public bool PrefillSystemCount { get; set; }
        public string GeneratedBy { get; set; } = string.Empty;

        public string MaterialCode => PartNumber;
    }
}
