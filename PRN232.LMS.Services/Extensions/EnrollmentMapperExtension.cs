using PRN232.LMS.Models.Entities;
using PRN232.LMS.Models.ResponseModel;

namespace PRN232.LMS.Services.Extensions
{
    public static class EnrollmentMapperExtension
    {
        public static EnrollmentResponse ToEnrollmentResponse(this Enrollment enrollment)
        {
            return new EnrollmentResponse
            {
                EnrollmentId = enrollment.Enrollmentid,
                EnrollDate = enrollment.Enrolldate,
                Status = enrollment.Status,
                Student = new StudentInEnrollmentResponse
                {
                    StudentId = enrollment.Student.Studentid,

                    FullName = enrollment.Student.Fullname,

                    Email = enrollment.Student.Email
                },
                Course = new CourseInEnrollmentResponse
                {
                    CourseId = enrollment.Course.Courseid,

                    CourseName = enrollment.Course.Coursename
                }
            };
        }

        public static List<EnrollmentResponse> ToEnrollmentResponseList(
      this List<Enrollment> enrollments)
        {
            return enrollments.Select(x => x.ToEnrollmentResponse())
                              .ToList();
        }
    }
}