using System;
using System.Collections.Generic;

namespace LMMWebAPI.DataAccess;

public partial class User
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string UserCode { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Assignment> Assignments { get; } = new List<Assignment>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Submission> Submissions { get; } = new List<Submission>();

    public virtual ICollection<Class> Classes { get; } = new List<Class>();
}
