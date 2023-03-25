using System;
using System.Collections.Generic;

namespace LMM_WebClient.Entity;

public partial class Grade
{
    public int GradeId { get; set; }

    public int SubmissionId { get; set; }

    public double Grade1 { get; set; }

    public string? Feedback { get; set; }

    public virtual Submission Submission { get; set; } = null!;
}
