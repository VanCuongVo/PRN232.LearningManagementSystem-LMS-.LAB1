using System.ComponentModel.DataAnnotations;

namespace PRN232.LMS.Models.RequestModel
{
    public class UpdateStudentRequest
    {
        [Required(ErrorMessage = "FullName is required")]
        public required string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public required string Email { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}