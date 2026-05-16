namespace PRN232.LMS.Models.RequestModel
{
    public class CreateStudentRequest
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}