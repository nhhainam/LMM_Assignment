using System;
using System.Collections.Generic;

namespace LMM_WebClient.Entity;

public partial class Assignment
{
    public int AssignmentId { get; set; }

    public int ClassId { get; set; }

    public int OwnerId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? Deadline { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual User Owner { get; set; } = null!;

    public virtual ICollection<Submission> Submissions { get; } = new List<Submission>();
}
