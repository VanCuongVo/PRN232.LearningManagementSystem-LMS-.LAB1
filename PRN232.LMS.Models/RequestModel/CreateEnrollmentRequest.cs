namespace PRN232.LMS.Models.RequestModel
{
    public class CreateEnrollmentRequest
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public DateTime EnrollDate { get; set; }

        public string Status { get; set; } = null!;
    }
}