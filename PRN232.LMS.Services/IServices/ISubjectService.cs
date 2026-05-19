using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Models.ResponseModel;

namespace PRN232.LMS.Services
{
    public interface ISubjectService
    {
        Task<ApiResponse<IEnumerable<SubjectResponse>>> GetAllAsync(QueryParameters query);
        Task<SubjectResponse> GetByIdAysnc(int id);
    }
}