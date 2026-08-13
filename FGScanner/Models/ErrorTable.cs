using System;
using System.Collections.Generic;

namespace FGScanner.Models;

public partial class ErrorTable
{
    public int Id { get; set; }

    public int ErrorId { get; set; }

    public string Message { get; set; }

    public string Stacktrace { get; set; }

    public DateTime Date { get; set; }

    public DateTime Time { get; set; }
}
