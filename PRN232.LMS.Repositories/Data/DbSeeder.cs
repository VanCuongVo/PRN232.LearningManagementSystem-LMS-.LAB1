using PRN232.LMS.Models.Entities;

namespace PRN232.LMS.Repositories.Data
{
    public static class DbSeeder
    {
        public static void Seed(LmsdbContext context)
        {
            // =========================
            // SEMESTERS
            // =========================
            if (!context.Semesters.Any())
            {
                context.Semesters.AddRange(
                    new Semester
                    {
                        Semestername = "Spring 2026",

                        Startdate = DateTime.SpecifyKind(
                            new DateTime(2026, 1, 1),
                            DateTimeKind.Utc),

                        Enddate = DateTime.SpecifyKind(
                            new DateTime(2026, 5, 1),
                            DateTimeKind.Utc)
                    },

                    new Semester
                    {
                        Semestername = "Summer 2026",

                        Startdate = DateTime.SpecifyKind(
                            new DateTime(2026, 6, 1),
                            DateTimeKind.Utc),

                        Enddate = DateTime.SpecifyKind(
                            new DateTime(2026, 9, 1),
                            DateTimeKind.Utc)
                    },

                    new Semester
                    {
                        Semestername = "Fall 2026",

                        Startdate = DateTime.SpecifyKind(
                            new DateTime(2026, 9, 2),
                            DateTimeKind.Utc),

                        Enddate = DateTime.SpecifyKind(
                            new DateTime(2026, 12, 31),
                            DateTimeKind.Utc)
                    }
                );

                context.SaveChanges();
            }

            // =========================
            // SUBJECTS
            // =========================
            if (!context.Subjects.Any())
            {
                context.Subjects.AddRange(
                    new Subject
                    {
                        Subjectcode = "PRN232",
                        Subjectname = "Web API",
                        Credit = 3
                    },

                    new Subject
                    {
                        Subjectcode = "SWP391",
                        Subjectname = "Software Project",
                        Credit = 4
                    },

                    new Subject
                    {
                        Subjectcode = "DBI202",
                        Subjectname = "Database Systems",
                        Credit = 3
                    },

                    new Subject
                    {
                        Subjectcode = "MAD101",
                        Subjectname = "Mobile Development",
                        Credit = 3
                    },

                    new Subject
                    {
                        Subjectcode = "OSG202",
                        Subjectname = "Operating Systems",
                        Credit = 2
                    },

                    new Subject
                    {
                        Subjectcode = "CSD201",
                        Subjectname = "Data Structures",
                        Credit = 3
                    },

                    new Subject
                    {
                        Subjectcode = "PRJ301",
                        Subjectname = "Java Web",
                        Credit = 3
                    },

                    new Subject
                    {
                        Subjectcode = "SWR302",
                        Subjectname = "Software Requirements",
                        Credit = 2
                    },

                    new Subject
                    {
                        Subjectcode = "MOB103",
                        Subjectname = "Flutter Basic",
                        Credit = 3
                    },

                    new Subject
                    {
                        Subjectcode = "NET181",
                        Subjectname = "Network Fundamentals",
                        Credit = 2
                    }
                );

                context.SaveChanges();
            }

            // =========================
            // STUDENTS
            // =========================
            if (!context.Students.Any())
            {
                var students = new List<Student>();

                for (int i = 1; i <= 100; i++)
                {
                    students.Add(new Student
                    {
                        Fullname = $"Student {i}",

                        Email = $"student{i}@gmail.com",

                        Dateofbirth = DateTime.SpecifyKind(
                            new DateTime(2000, 1, 1)
                                .AddDays(i * 30),
                            DateTimeKind.Utc)
                    });
                }

                context.Students.AddRange(students);

                context.SaveChanges();
            }

            // =========================
            // COURSES
            // =========================
            if (!context.Courses.Any())
            {
                var springSemester = context.Semesters
                    .First(x => x.Semestername == "Spring 2026");

                var summerSemester = context.Semesters
                    .First(x => x.Semestername == "Summer 2026");

                var fallSemester = context.Semesters
                    .First(x => x.Semestername == "Fall 2026");

                context.Courses.AddRange(

                    // SPRING
                    new Course
                    {
                        Coursename = "PRN232 API Spring",
                        Semesterid = springSemester.Semesterid
                    },

                    new Course
                    {
                        Coursename = "DBI202 Database Spring",
                        Semesterid = springSemester.Semesterid
                    },

                    new Course
                    {
                        Coursename = "CSD201 DSA Spring",
                        Semesterid = springSemester.Semesterid
                    },

                    // SUMMER
                    new Course
                    {
                        Coursename = "SWP391 Project Summer",
                        Semesterid = summerSemester.Semesterid
                    },

                    new Course
                    {
                        Coursename = "MAD101 Mobile Summer",
                        Semesterid = summerSemester.Semesterid
                    },

                    new Course
                    {
                        Coursename = "MOB103 Flutter Summer",
                        Semesterid = summerSemester.Semesterid
                    },

                    // FALL
                    new Course
                    {
                        Coursename = "NET181 Network Fall",
                        Semesterid = fallSemester.Semesterid
                    },

                    new Course
                    {
                        Coursename = "OSG202 OS Fall",
                        Semesterid = fallSemester.Semesterid
                    },

                    new Course
                    {
                        Coursename = "PRJ301 Java Web Fall",
                        Semesterid = fallSemester.Semesterid
                    }
                );

                context.SaveChanges();
            }

            // =========================
            // ENROLLMENTS
            // =========================
            if (!context.Enrollments.Any())
            {
                var students = context.Students.ToList();

                var courses = context.Courses.ToList();

                var enrollments = new List<Enrollment>();

                var random = new Random();

                var statuses = new[]
                {
                    "Studying",
                    "Completed",
                    "Pending",
                    "Dropped"
                };

                foreach (var student in students)
                {
                    var randomCourses = courses
                        .OrderBy(x => Guid.NewGuid())
                        .Take(3)
                        .ToList();

                    foreach (var course in randomCourses)
                    {
                        enrollments.Add(new Enrollment
                        {
                            Studentid = student.Studentid,

                            Courseid = course.Courseid,

                            Enrolldate = DateTime.SpecifyKind(
                                DateTime.UtcNow.AddDays(
                                    -random.Next(1, 100)),
                                DateTimeKind.Utc),

                            Status = statuses[
                                random.Next(statuses.Length)
                            ]
                        });
                    }
                }

                context.Enrollments.AddRange(enrollments);

                context.SaveChanges();
            }
        }
    }
}