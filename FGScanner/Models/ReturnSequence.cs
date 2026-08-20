using System;
using System.Collections.Generic;

namespace FGScanner.Models;

public partial class ReturnSequence
{
    public DateOnly SeqDate { get; set; }

    public int LastNumber { get; set; }
}
