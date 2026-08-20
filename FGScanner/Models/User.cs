using System;
using System.Collections.Generic;

namespace FGScanner.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public string Password { get; set; }

    public string Name { get; set; }

    public string Role { get; set; }

    public int GroupId { get; set; }

    public string GroupCategory { get; set; }

    public string Status { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? LastActive { get; set; }

    public virtual UserGroup Group { get; set; }

    public virtual ICollection<UserInformation> UserInformations { get; set; } = new List<UserInformation>();
}
