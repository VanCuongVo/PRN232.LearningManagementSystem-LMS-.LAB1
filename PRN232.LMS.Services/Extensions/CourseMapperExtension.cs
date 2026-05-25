using PRN232.LMS.Models.Entities;
using PRN232.LMS.Models.ResponseModel;

namespace PRN232.LMS.Services.Extensions
{
    public static class CourseMapperExtension
    {
        public static CourseResponse ToCourseResponse(this Course course)
        {
            return new CourseResponse
            {
                CourseId = course.Courseid,
                CourseName = course.Coursename,
                SemesterId = course.Semester.Semesterid,
                SemesterName = course.Semester.Semestername,
                Enrollments = course.Enrollments.Select(x => new EnrollmentInCourseResponse
                {
                    EnrollmentId = x.Enrollmentid,
                    EnrollDate = x.Enrolldate,
                    Status = x.Status ?? string.Empty
                    // StudentId = x.Studentid,
                    // StudentName = x.Student?.Fullname ?? string.Empty
                }).ToList(),
                Students = course.Enrollments.Where(x => x.Student != null).Select(x => new StudentInCourseResponse
                {
                    StudentId = x.Student!.Studentid,
                    FullName = x.Student.Fullname,
                    Email = x.Student.Email
                }).ToList()
            };
        }

        public static List<CourseResponse> ToCourseResponseList(
          this List<Course> courses)
        {
            return courses
                .Select(ToCourseResponse)
                .ToList();
        }
    }
}