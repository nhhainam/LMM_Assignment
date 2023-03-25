using System;
using System.Collections.Generic;

namespace BusinessObjects.DataAccess;

public partial class Submission
{
    public int SubmissionId { get; set; }

    public int AssignmentId { get; set; }

    public int OwnerId { get; set; }

    public DateTime? SubmissionTime { get; set; }

    public string? FilePath { get; set; }

    public virtual Assignment Assignment { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; } = new List<Grade>();

    public virtual User Owner { get; set; } = null!;
}
