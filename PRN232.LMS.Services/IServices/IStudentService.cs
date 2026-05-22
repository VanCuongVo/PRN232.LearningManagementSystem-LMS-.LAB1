using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Models.ResponseModel;

namespace PRN232.LMS.Services.IServices
{
    public interface IStudentService
    {
        Task<ApiResponse<object>> GetAllAsync(QueryParameters query);
        Task<StudentResponse> GetByIdAsync(int id);
        Task<ApiResponse<StudentResponse>> CreateAsync(CreateStudentRequest request);

        Task<ApiResponse<StudentResponse>> UpdateAsync(int id, UpdateStudentRequest request);
        Task<ApiResponse<bool>> DeleteAsync(int id);



    }
}
