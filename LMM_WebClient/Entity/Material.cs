﻿using System;
using System.Collections.Generic;

namespace LMM_WebClient.Entity;

public partial class Material
{
    public int MaterialId { get; set; }

    public int ClassId { get; set; }

    public string Title { get; set; } = null!;

    public string? FilePath { get; set; }

    public virtual Class Class { get; set; } = null!;
}
