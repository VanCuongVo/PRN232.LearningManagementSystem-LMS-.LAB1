using PRN232.LMS.Models.Entities;
using PRN232.LMS.Models.RequestModel;

namespace PRN232.LMS.Services.Utility
{
    public static class SemesterQueryExtensions
    {
        public static IQueryable<Semester> Search(this IQueryable<Semester> query, QueryParameters request)
        {
            if (string.IsNullOrWhiteSpace(request.Search))
            {
                return query;
            }

            var search = request.Search.ToLower();
            return query.Where(x => x.Semestername.ToLower().Contains(search));
        }


        public static IQueryable<Semester> Sort(this IQueryable<Semester> query, QueryParameters request)
        {

            return request.Sort switch
            {
                "semestername" => query.OrderBy(x => x.Semestername),
                "-semestername" => query.OrderByDescending(x => x.Semestername),
                _ => query.OrderBy(x => x.Semesterid)
            };
        }

        public static IQueryable<Semester> Paging(
                   this IQueryable<Semester> query,
                   QueryParameters request)
        {
            return query.Skip((request.Page - 1) * request.Size).Take(request.Size);
        }
    }
}