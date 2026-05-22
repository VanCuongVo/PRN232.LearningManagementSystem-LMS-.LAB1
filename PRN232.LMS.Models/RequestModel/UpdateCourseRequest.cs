namespace PRN232.LMS.Models.RequestModel
{
    public class UpdateCourseRequest
    {
        public string CourseName { get; set; } = string.Empty;

        public int SemesterId { get; set; }
    }
}