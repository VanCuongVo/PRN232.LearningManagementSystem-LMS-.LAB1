using System.ComponentModel.DataAnnotations;

namespace PRN232.LMS.Models.RequestModel
{
    public class UpdateCourseRequest
    {
        [Required(ErrorMessage = "CourseName is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "CourseName must be between 2 and 100 characters")]
        public string CourseName { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "SemesterId must be a positive integer")]
        public int SemesterId { get; set; }
    }
}