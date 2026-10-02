using System;
using System.Collections.Generic;

namespace FGScanner.Models;

public class InventoryCountBatch
{
    public int Id { get; set; }
    public string BatchCode { get; set; } = string.Empty;
    public DateOnly CutoffDate { get; set; }
    public string WarehouseId { get; set; } = string.Empty;
    public string GenerationMode { get; set; } = string.Empty;
    public string RackLocation { get; set; } = string.Empty;
    public int Revision { get; set; }
    public int BatchSequence { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public bool PrefillSystemCount { get; set; }
    public int TagCount { get; set; }
    public string Status { get; set; } = string.Empty;

    public virtual ICollection<InventoryCountTagSnapshot> Tags { get; set; } =
        new List<InventoryCountTagSnapshot>();
}
