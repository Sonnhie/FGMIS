using System;
using System.Collections.Generic;

namespace FGScanner.Models;

public partial class TransactionHistoryClean
{
    public int Id { get; set; }

    public string Partnumber { get; set; }

    public DateOnly ProdDate { get; set; }

    public string CustomerId { get; set; }

    public int Quantity { get; set; }

    public int? Box { get; set; }

    public string ProdVer { get; set; }

    public DateTime EntryDate { get; set; }

    public string TransactionType { get; set; }

    public string Location { get; set; }

    public string Remarks { get; set; }

    public string Status { get; set; }

    public string StorageLocation { get; set; }

    public string ControlNumber { get; set; }

    public string WhId { get; set; }

    public Guid? TransactionId { get; set; }

    public bool? IsSynced { get; set; }

    public int? SyncStatus { get; set; }

    public string InCharge { get; set; }
}
