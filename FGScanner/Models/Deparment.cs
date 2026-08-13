using System;
using System.Collections.Generic;

namespace FGScanner.Models;

public partial class Deparment
{
    public string DeptId { get; set; }

    public string DeptGroup { get; set; }

    public virtual ICollection<UserInformation> UserInformations { get; set; } = new List<UserInformation>();
}
