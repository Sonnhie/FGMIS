using System;
using System.Collections.Generic;

namespace FGScanner.Models;

public partial class InventoryRebuildLog
{
    public int Id { get; set; }

    public DateTime? RebuildDate { get; set; }

    public string RebuiltBy { get; set; }
}
