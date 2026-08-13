using System;
using System.Collections.Generic;

namespace FGScanner.Models;

public partial class Module
{
    public int ModuleId { get; set; }

    public string ModuleName { get; set; }

    public string Description { get; set; }

    public bool IsActive { get; set; }
}
