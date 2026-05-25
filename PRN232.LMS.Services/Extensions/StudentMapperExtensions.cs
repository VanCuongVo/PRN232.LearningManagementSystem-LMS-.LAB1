using PRN232.LMS.Models.Entities;
using PRN232.LMS.Models.ResponseModel;

namespace PRN232.LMS.Services.Extensions
{
    public static class StudentMapperExtensions
    {
        public static StudentResponse ToStudentResponse(
         this Student student)
        {
            return new StudentResponse
            {
                StudentId = student.Studentid,

                FullName = student.Fullname,

                Email = student.Email,

                DateOfBirth = student.Dateofbirth,
                Enrollments = student.Enrollments.Select(x => new EnrollmentResponse
                {
                    EnrollmentId = x.Enrollmentid,
                    Status = x.Status,
                    EnrollDate = x.Enrolldate,
                    // CourseId = x.Course.Courseid,
                    // CourseName = x.Course.Coursename,
                    // StudentId = x.Student.Studentid,
                    // StudentName = x.Student.Fullname,
                    // StudentEmail = x.Student.Email
                    Student = new StudentInEnrollmentResponse
                    {
                        StudentId = x.Student.Studentid,

                        FullName = x.Student.Fullname,

                        Email = x.Student.Email
                    },
                    Course = new CourseInEnrollmentResponse
                    {
                        CourseId = x.Course.Courseid,

                        CourseName = x.Course.Coursename
                    }
                }).ToList()
            };
        }
        public static List<StudentResponse> ToStudentResponseList(this IEnumerable<Student> students)
        {
            return students.Select(x => x.ToStudentResponse()).ToList();
        }
    }
}