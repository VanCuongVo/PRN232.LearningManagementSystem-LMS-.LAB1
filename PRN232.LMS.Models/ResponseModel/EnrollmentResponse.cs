
namespace PRN232.LMS.Models.ResponseModel
{
    public class EnrollmentResponse
    {
        public int EnrollmentId { get; set; }

        public DateTime EnrollDate { get; set; }

        public string Status { get; set; } = null!;

        public StudentInEnrollmentResponse? Student { get; set; }

        public CourseInEnrollmentResponse? Course { get; set; }
    }
}