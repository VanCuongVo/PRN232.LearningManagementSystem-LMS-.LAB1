namespace PRN232.LMS.Models.RequestModel
{
    public class UpdateStudentRequest
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}