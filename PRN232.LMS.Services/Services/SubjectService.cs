using Microsoft.EntityFrameworkCore;
using PRN232.LMS.Models.Entities;
using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Models.ResponseModel;
using PRN232.LMS.Repositories.IRepositories;
using PRN232.LMS.Services.Extensions;
using PRN232.LMS.Services.Utility;

namespace PRN232.LMS.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<IEnumerable<SubjectResponse>>> GetAllAsync(QueryParameters query)
        {
            IQueryable<Subject> subjectQuery = _unitOfWork.Subjects.GetQueryable();

            // Search 
            subjectQuery = StubjectQueryExtensions.Search(subjectQuery, query);

            // Sort 

            subjectQuery = StubjectQueryExtensions.Sort(subjectQuery, query);

            // Total Items
            int totalItems = await subjectQuery.CountAsync();


            subjectQuery = StubjectQueryExtensions.Paging(subjectQuery, query);


            List<Subject> subjects = await subjectQuery.ToListAsync();

            var response = SubjectMapperExtensions.ToSubjectResponseList(subjects);

            return new ApiResponse<IEnumerable<SubjectResponse>>
            {
                success = true,
                message = "Get subjects successfully",
                Data = response,
                pagination = new PaginationMetadata
                {
                    PageSize = query.Size,
                    Page = query.Page,
                    TotalItems = totalItems,
                    TotalPages = (int)Math.Ceiling((double)totalItems / query.Size),
                }
            };
        }

        public async Task<SubjectResponse> GetByIdAysnc(int id)
        {
            var existingSubjects = await _unitOfWork.Subjects.GetByIdAsync(id);
            if (existingSubjects == null)
            {
                return null;
            }
            var response = SubjectMapperExtensions.ToSubjectResponse(existingSubjects);
            return response;
        }
    }
}