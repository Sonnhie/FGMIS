using System;
using System.Collections.Generic;

namespace FGScanner.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Partnumber { get; set; }

    public string Partname { get; set; }

    public string CustomerId { get; set; }

    public int? Pps { get; set; }
}
