using System;
using System.Collections.Generic;

namespace FGScanner.Models;

public partial class UserInformation
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public string Name { get; set; }

    public string DepartmentId { get; set; }

    public int? GroupId { get; set; }

    public string Email { get; set; }

    public virtual Deparment Department { get; set; }

    public virtual UserGroup Group { get; set; }

    public virtual User User { get; set; }
}
