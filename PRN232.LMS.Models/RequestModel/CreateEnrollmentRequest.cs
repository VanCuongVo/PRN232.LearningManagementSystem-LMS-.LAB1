using System.ComponentModel.DataAnnotations;

namespace PRN232.LMS.Models.RequestModel
{
    public class CreateEnrollmentRequest
    {
        public int EnrollmentId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "StudentId must be a positive integer")]
        public int StudentId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "CourseId must be a positive integer")]
        public int CourseId { get; set; }

        public DateTime EnrollDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters")]
        public string Status { get; set; } = null!;
    }
}