using System;
using System.Collections.Generic;

namespace LMM_WebClient.Entity;

public partial class Class
{
    public int ClassId { get; set; }

    public string ClassCode { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Assignment> Assignments { get; } = new List<Assignment>();

    public virtual ICollection<Material> Materials { get; } = new List<Material>();
}
