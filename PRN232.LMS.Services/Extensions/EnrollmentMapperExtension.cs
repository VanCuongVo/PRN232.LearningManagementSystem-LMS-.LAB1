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
                StudentId = enrollment.Student.Studentid,
                StudentName = enrollment.Student.Fullname,
                StudentEmail = enrollment.Student.Email,
                CourseId = enrollment.Course.Courseid,
                CourseName = enrollment.Course.Coursename
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