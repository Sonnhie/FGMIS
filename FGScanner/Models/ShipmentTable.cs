using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FGScanner.Models;

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

    [ForeignKey("TransactionId")]
    public virtual ICollection<TransactionHistory> TransactionHistories { get; set; } = [];
}
