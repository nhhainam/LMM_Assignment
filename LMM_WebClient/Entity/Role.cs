using System;
using System.Collections.Generic;

namespace LMM_WebClient.Entity;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
