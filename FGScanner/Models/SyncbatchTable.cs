using System;
using System.Collections.Generic;

namespace FGScanner.Models;

public partial class SyncbatchTable
{
    public int Id { get; set; }

    public Guid SyncBatchId { get; set; }

    public string WhId { get; set; }

    public DateTime SyncDate { get; set; }

    public int TotalRecords { get; set; }
}
