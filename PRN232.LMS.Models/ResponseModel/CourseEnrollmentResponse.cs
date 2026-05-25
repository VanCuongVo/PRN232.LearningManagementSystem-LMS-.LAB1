namespace PRN232.LMS.Models.ResponseModel
{
    public class CourseEnrollmentResponse
    {
        public int EnrollmentId { get; set; }

        public DateTime EnrollDate { get; set; }

        public string? Status { get; set; }

        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public StudentInEnrollmentResponse? Student { get; set; }
    }
}