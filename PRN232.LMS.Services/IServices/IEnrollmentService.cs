using PRN232.LMS.Models.ResponseModel;

namespace PRN232.LMS.Services.Interfaces
{
    public interface IEnrollmentService
    {
        Task<ApiResponse<List<EnrollmentResponse>>> GetAllAsync();

        Task<EnrollmentResponse?> GetByIdAsync(int id);
    }
}