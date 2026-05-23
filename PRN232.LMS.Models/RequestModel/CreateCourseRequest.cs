namespace PRN232.LMS.Models.RequestModel
{
    public class CreateCourseRequest
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; } = string.Empty;

        public int SemesterId { get; set; }
    }
}