
namespace PRN232.LMS.Models.ResponseModel
{
    public class EnrollmentResponse
    {
        public int EnrollmentId { get; set; }

        public DateTime EnrollDate { get; set; }

        public string Status { get; set; } = null!;

        // Student Info
        public int StudentId { get; set; }

        public string StudentName { get; set; } = null!;

        public string StudentEmail { get; set; } = null!;

        // Course Info
        public int CourseId { get; set; }

        public string CourseName { get; set; } = null!;
    }
}