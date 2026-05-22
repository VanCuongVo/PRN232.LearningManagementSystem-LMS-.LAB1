namespace PRN232.LMS.Models.RequestModel
{
    public class CreateSemesterRequest
    {
        public int Semesterid { get; set; }
        public string SemesterName { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}