using System;
using System.Collections.Generic;

namespace FGScanner.DTOs;

public sealed class InventoryCheckTagBatchData
{
    public int BatchId { get; set; }
    public string BatchCode { get; set; } = string.Empty;
    public int Revision { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public List<InventoryCheckTagData> Tags { get; set; } = new();
}

public sealed class InventoryCheckTagBatchSummary
{
    public int BatchId { get; set; }
    public string BatchCode { get; set; } = string.Empty;
    public DateOnly CutoffDate { get; set; }
    public string Warehouse { get; set; } = string.Empty;
    public string Scope { get; set; } = string.Empty;
    public int Revision { get; set; }
    public int TagCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}
