using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Models.ResponseModel;

namespace PRN232.LMS.Services.Services
{
    public interface ISemestersService
    {
        Task<ApiResponse<List<SemesterResponse>>> GetAllAsync(QueryParameters query);
        Task<SemesterResponse> GetByIdAsync(int id);
        Task<ApiResponse<SemesterResponse>> CreateAsync(CreateSemesterRequest request);

        Task<ApiResponse<SemesterResponse>> UpdateAsync(int id, UpdateSemesterRequest request);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}