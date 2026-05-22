using PRN232.LMS.Models.ResponseModel;
using PRN232.LMS.Services.Interfaces;

namespace PRN232.LMS.Services.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        public Task<ApiResponse<List<EnrollmentResponse>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EnrollmentResponse?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}