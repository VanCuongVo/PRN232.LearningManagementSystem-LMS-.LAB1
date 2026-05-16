using Microsoft.EntityFrameworkCore;
using PRN232.LMS.Models.Entities;
using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Models.ResponseModel;
using PRN232.LMS.Repositories.IRepositories;
using PRN232.LMS.Services.IServices;

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
            if (!string.IsNullOrEmpty(query.Search))
            {
                studentsQuery = studentsQuery.Where(x => x.Fullname.ToLower().Contains(query.Search.ToLower()));
            }

            // Sort
            switch (query.Sort)
            {
                case "fullName":
                    studentsQuery = studentsQuery.OrderBy(x => x.Fullname);
                    break;

                case "_fullName":
                    studentsQuery = studentsQuery.OrderByDescending(x => x.Fullname);
                    break;

                case "dateOfBirth":
                    studentsQuery = studentsQuery.OrderBy(x => x.Dateofbirth);
                    break;

                case "_dateOfBirth":
                    studentsQuery = studentsQuery.OrderByDescending(x => x.Dateofbirth);
                    break;

                default:
                    studentsQuery = studentsQuery
                        .OrderBy(x => x.Studentid);
                    break;
            }
            // TOTAL ITEMS
            var totalItems =
                await studentsQuery.CountAsync();

            // Pading
            studentsQuery = studentsQuery
    .Skip((query.Page - 1) * query.Size)
    .Take(query.Size);
            var students =
               await studentsQuery.ToListAsync();

            var response = students.Select(x => new StudentResponse
            {
                StudentId = x.Studentid,
                FullName = x.Fullname,
                Email = x.Email,
                DateOfBirth = x.Dateofbirth
            }).ToList();

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
