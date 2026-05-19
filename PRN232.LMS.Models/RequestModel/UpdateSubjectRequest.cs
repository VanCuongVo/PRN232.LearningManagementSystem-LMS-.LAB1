namespace PRN232.LMS.Models.RequestModel
{
    public class UpdateSubjectRequest
    {
        public string SubjectCode { get; set; } = null!;

        public string SubjectName { get; set; } = null!;

        public int Credit { get; set; }
    }
}