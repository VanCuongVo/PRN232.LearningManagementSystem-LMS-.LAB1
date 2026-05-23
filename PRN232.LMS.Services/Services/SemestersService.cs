using Microsoft.EntityFrameworkCore;
using PRN232.LMS.Models.Entities;
using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Models.ResponseModel;
using PRN232.LMS.Repositories.IRepositories;
using PRN232.LMS.Services.Extensions;
using PRN232.LMS.Services.Utility;

namespace PRN232.LMS.Services.Services
{
    public class SemestersService : ISemestersService
    {
        private readonly IUnitOfWork _uniOfWork;

        public SemestersService(IUnitOfWork unitOfWork)
        {
            _uniOfWork = unitOfWork;
        }

        public async Task<ApiResponse<SemesterResponse>> CreateAsync(CreateSemesterRequest request)
        {
            var createSemestesRequest = new Semester
            {
                Semesterid = request.SemesterId,
                Semestername = request.SemesterName,
                Enddate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc),
                Startdate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc),
            };

            await _uniOfWork.Semesters.AddAsync(createSemestesRequest);
            await _uniOfWork.SaveChangesAsync();


            return new ApiResponse<SemesterResponse>
            {
                success = true,

                message = "Create semester successfully",

                Data = createSemestesRequest.ToSemesterReponse()
            };
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var semester = await _uniOfWork.Semesters.GetByIdAsync(id);

            if (semester != null)
            {
                await _uniOfWork.Semesters.DeleteAsync(
                    semester.Semesterid);

                await _uniOfWork.SaveChangesAsync();

                return new ApiResponse<bool>
                {
                    success = true,
                    message = "Delete semester successfully",
                    Data = true
                };
            }

            return new ApiResponse<bool>
            {
                success = false,
                message = "Delete semester fails",
                Data = false
            };
        }

        public async Task<ApiResponse<object>> GetAllAsync(QueryParameters query)
        {
            var semesterQuery = _uniOfWork.Semesters.GetQueryable().Search(query).Sort(query).Paging(query).Expand(query);
            var totalItems = await semesterQuery.CountAsync();

            var semesters = await semesterQuery.ToListAsync();

            var responses = semesters.ToSemesterReponseList();
            var shapedData = responses.SelectFields(query.Fields);

            return new ApiResponse<object>
            {
                success = true,
                message = "Get semester successfully",
                Data = shapedData,
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

        public async Task<SemesterResponse> GetByIdAsync(int id)
        {

            var existingSemester = await _uniOfWork.Semesters.GetByIdAsync(id);
            if (existingSemester == null)
            {
                return null;
            }
            var repsone = SemesterMapperExtension.ToSemesterReponse(existingSemester);
            return repsone;

        }

        public async Task<ApiResponse<SemesterResponse>> UpdateAsync(int id, UpdateSemesterRequest request)
        {
            var existingSemester = await _uniOfWork.Semesters.GetByIdAsync(id);
            if (existingSemester == null)
            {
                return new ApiResponse<SemesterResponse>
                {
                    success = false,
                    message =
               "Semester not found"
                };
            }

            existingSemester.Semestername = request.SemesterName;
            existingSemester.Enddate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc);
            existingSemester.Startdate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);


            await _uniOfWork.Semesters.UpdateAsync(existingSemester);
            await _uniOfWork.SaveChangesAsync();

            return new ApiResponse<
        SemesterResponse>
            {
                success = true,

                message =
            "Update semester successfully",

                Data = existingSemester.ToSemesterReponse()
            };
        }
    }
}