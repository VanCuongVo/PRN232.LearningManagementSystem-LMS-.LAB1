using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Models.ResponseModel;

namespace PRN232.LMS.Services.IServices
{
    public interface IStudentService
    {
        Task<ApiResponse<List<StudentResponse>>> GetAllAsync(StudentQueryParameters query);
    }
}
