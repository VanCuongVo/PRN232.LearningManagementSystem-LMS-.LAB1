using Microsoft.EntityFrameworkCore;
using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Models.ResponseModel;
using PRN232.LMS.Repositories.IRepositories;
using PRN232.LMS.Services.Extensions;
using PRN232.LMS.Services.IServices;
using PRN232.LMS.Services.Utility;

namespace PRN232.LMS.Services.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<List<StudentResponse>>> GetAllAsync(StudentQueryParameters query)
        {
            var studentsQuery = _unitOfWork
                            .Students
                            .GetQueryable();

            // Search 
            studentsQuery = StudentQueryExtensions.Search(studentsQuery, query);
            // Sort
            studentsQuery = StudentQueryExtensions.Sort(studentsQuery,
            query);
            // TOTAL ITEMS
            var totalItems =
                await studentsQuery.CountAsync();

            // Pading
            studentsQuery = StudentQueryExtensions.Paging(studentsQuery, query);

            var students =
               await studentsQuery.ToListAsync();

            var response = StudentMapperExtensions.ToStudentResponseList(students);

            return new ApiResponse<List<StudentResponse>>
            {
                success = true,
                message = "Get students successfully",
                Data = response,
                pagination = new PaginationMetadata
                {
                    Page = query.Page,
                    PageSize = query.Size,
                    TotalItems = totalItems,
                    TotalPages =
                            (int)Math.Ceiling(
                                (double)totalItems
                                / query.Size)
                }
            };
        }
    }
}
