using PRN232.LMS.Models.Entities;
using PRN232.LMS.Models.ResponseModel;

namespace PRN232.LMS.Services.Extensions
{
    public static class SemesterMapperExtension
    {
        public static SemesterResponse ToSemesterReponse(this Semester semesters)
        {
            return new SemesterResponse
            {
                SemesterId = semesters.Semesterid,
                SemesterName = semesters.Semestername,
                StartDate = semesters.Startdate,
                EndDate = semesters.Enddate,
                Courses = semesters.Courses.Select(x => new CourseResponse
                {
                    CourseId = x.Courseid,
                    CourseName = x.Coursename
                }).ToList()
            };
        }

        public static List<SemesterResponse> ToSemesterReponseList(this List<Semester> semesters)
        {
            return semesters.Select(x => x.ToSemesterReponse()).ToList();
        }

    }
}