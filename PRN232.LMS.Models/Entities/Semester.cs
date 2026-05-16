using System;
using System.Collections.Generic;

namespace PRN232.LMS.Models.Entities;

public partial class Semester
{
    public int Semesterid { get; set; }

    public string Semestername { get; set; } = null!;

    public DateTime Startdate { get; set; }

    public DateTime Enddate { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
