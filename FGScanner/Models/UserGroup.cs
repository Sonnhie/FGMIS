using System;
using System.Collections.Generic;

namespace FGScanner.Models;

public partial class UserGroup
{
    public int GroupId { get; set; }

    public string GroupName { get; set; }

    public virtual ICollection<UserInformation> UserInformations { get; set; } = new List<UserInformation>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
