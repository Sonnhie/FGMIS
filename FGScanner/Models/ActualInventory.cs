using System;
using System.Collections.Generic;

namespace FGScanner.Models;

public partial class ActualInventory
{
    public int Id { get; set; }

    public string Partnumber { get; set; }

    public string Customer { get; set; }

    public DateOnly? ProdDate { get; set; }

    public string ProdVer { get; set; }

    public string Location { get; set; }

    public int? TotalBox { get; set; }

    public int? Quantity { get; set; }

    public string StorageLocation { get; set; }

    public string WhId { get; set; }

    public string Remarks { get; set; }

    public DateTime? LastInDate { get; set; }

    public DateTime? LastOutDate { get; set; }

    public int? IdleDays { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string MovementClassification { get; set; }
}
