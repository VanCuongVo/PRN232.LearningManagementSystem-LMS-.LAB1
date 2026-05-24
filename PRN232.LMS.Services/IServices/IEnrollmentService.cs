using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Models.ResponseModel;

namespace PRN232.LMS.Services.Interfaces
{
    public interface IEnrollmentService
    {
        Task<ApiResponse<object>> GetAllAsync(QueryParameters query);

        Task<EnrollmentResponse?> GetByIdAsync(int id);
        Task<ApiResponse<EnrollmentResponse>> CreateAsync(
       CreateEnrollmentRequest request);

        Task<ApiResponse<EnrollmentResponse?>> UpdateAsync(
            int id,
            UpdateEnrollmentRequest request);
    }
}