namespace PRN232.LMS.Models.ResponseModel
{
    public class EnrollmentInCourseResponse
    {
        public int EnrollmentId { get; set; }

        public DateTime EnrollDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public int StudentId { get; set; }

        public string StudentName { get; set; } = string.Empty;


    }
}