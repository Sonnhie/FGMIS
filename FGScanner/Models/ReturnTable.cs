using System;
using System.Collections.Generic;

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

    public virtual ICollection<TransactionHistory> TransactionHistories { get; set; } = new List<TransactionHistory>();
}
