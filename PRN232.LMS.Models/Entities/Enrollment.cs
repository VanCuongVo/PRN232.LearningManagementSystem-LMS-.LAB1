using System;
using System.Collections.Generic;

namespace PRN232.LMS.Models.Entities;

public partial class Enrollment
{
    public int Enrollmentid { get; set; }

    public int Studentid { get; set; }

    public int Courseid { get; set; }

    public DateTime Enrolldate { get; set; }

    public string Status { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
