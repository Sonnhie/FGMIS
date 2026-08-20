using System;
using System.Collections.Generic;

namespace FGScanner.Models.Temp;

public partial class ShipmentTable
{
    public int Id { get; set; }

    public string TransactionId { get; set; }

    public DateTime EntryDate { get; set; }

    public string Status { get; set; }

    public string Customer { get; set; }

    public string WhId { get; set; }

    public Guid? ShipmentId { get; set; }

    public bool? IsSynced { get; set; }

    public int? SyncStatus { get; set; }
}
