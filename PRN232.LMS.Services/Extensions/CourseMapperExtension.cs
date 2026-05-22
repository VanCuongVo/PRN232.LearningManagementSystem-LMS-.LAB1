using PRN232.LMS.Models.Entities;
using PRN232.LMS.Models.ResponseModel;

namespace PRN232.LMS.Services.Extensions
{
    public static class CourseMapperExtension
    {
        public static CourseResponse ToCourseResponse(Course course)
        {
            return new CourseResponse
            {
                CourseId = course.Courseid,
                CourseName = course.Coursename,
                SemesterId = course.Semesterid,
                SemesterName = course.Semester?.Semestername ?? string.Empty
            };
        }

        public static List<CourseResponse> ToCourseResponseList(
           List<Course> courses)
        {
            return courses
                .Select(ToCourseResponse)
                .ToList();
        }
    }
}