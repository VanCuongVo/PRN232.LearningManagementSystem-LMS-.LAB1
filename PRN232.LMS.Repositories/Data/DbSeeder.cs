using PRN232.LMS.Models.Entities;

namespace PRN232.LMS.Repositories.Data
{
    public static class DbSeeder
    {
        public static void Seed(LmsdbContext context)
        {
            // SUBJECTS
            if (!context.Subjects.Any())
            {
                context.Subjects.AddRange(
                    new Subject { Subjectcode = "PRN232", Subjectname = "Web API", Credit = 3 },
                    new Subject { Subjectcode = "SWP391", Subjectname = "Software Project", Credit = 4 },
                    new Subject { Subjectcode = "DBI202", Subjectname = "Database Systems", Credit = 3 },
                    new Subject { Subjectcode = "MAD101", Subjectname = "Mobile Development", Credit = 3 },
                    new Subject { Subjectcode = "OSG202", Subjectname = "Operating Systems", Credit = 2 },
                    new Subject { Subjectcode = "CSD201", Subjectname = "Data Structures", Credit = 3 },
                    new Subject { Subjectcode = "PRJ301", Subjectname = "Java Web", Credit = 3 },
                    new Subject { Subjectcode = "SWR302", Subjectname = "Software Requirements", Credit = 2 },
                    new Subject { Subjectcode = "MOB103", Subjectname = "Flutter Basic", Credit = 3 },
                    new Subject { Subjectcode = "NET181", Subjectname = "Network Fundamentals", Credit = 2 }
                );

                context.SaveChanges();
            }

            // STUDENTS
            if (!context.Students.Any())
            {
                var students = new List<Student>();

                for (int i = 1; i <= 20; i++)
                {
                    students.Add(new Student
                    {
                        Fullname = $"Student {i}",
                        Email = $"student{i}@gmail.com",
                        Dateofbirth = DateTime.SpecifyKind(
            new DateTime(2004, 1, 1).AddDays(i),
            DateTimeKind.Utc
        )
                    });
                }
                context.Students.AddRange(students);
                context.SaveChanges();
            }
        }
    }
}