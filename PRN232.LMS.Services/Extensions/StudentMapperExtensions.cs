

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

                DateOfBirth =
                    student.Dateofbirth

            };
        }
        public static List<StudentResponse> ToStudentResponseList(this IEnumerable<Student> students)
        {
            return students.Select(x => x.ToStudentResponse()).ToList();
        }

    }
}