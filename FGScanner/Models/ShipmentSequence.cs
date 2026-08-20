using System;
using System.Collections.Generic;

namespace FGScanner.Models;

public partial class ShipmentSequence
{
    public DateOnly SeqDate { get; set; }

    public int LastNumber { get; set; }
}
