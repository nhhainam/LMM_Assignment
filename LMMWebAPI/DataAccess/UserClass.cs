using System;
using System.Collections.Generic;

namespace LMMWebAPI.DataAccess;

public partial class UserClass
{
    public int UserId { get; set; }

    public int ClassId { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
