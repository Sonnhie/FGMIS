using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FGScanner.Models;

public partial class ReturnTable
{
    public int Id { get; set; }

    public string TransactionId { get; set; }

    public DateTime EntryDate { get; set; }

    public string FromLocation { get; set; }

    public string ToLocation { get; set; }

    public string Status { get; set; }

    public string Remarks { get; set; }

    public string WhId { get; set; }

    public Guid? ReturnId { get; set; }

    public bool? IsSynced { get; set; }

    public int? SyncStatus { get; set; }

    [NotMapped]
    public int Quantity { get; set; }

    [NotMapped]
    public int Box { get; set; }

    [ForeignKey("TransactionId")]
    public virtual ICollection<TransactionHistory> TransactionHistories { get; set; } = [];
}
