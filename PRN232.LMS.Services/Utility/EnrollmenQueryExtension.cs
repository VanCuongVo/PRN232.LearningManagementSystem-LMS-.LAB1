using PRN232.LMS.Models.Entities;
using PRN232.LMS.Models.RequestModel;

namespace PRN232.LMS.Services.Utility
{
    public static class EnrollmentExtensions
    {
        public static IQueryable<Enrollment> Search(
            this IQueryable<Enrollment> query,
            QueryParameters request)
        {
            if (string.IsNullOrEmpty(request.Search))
            {
                return query;
            }

            var search = request.Search.ToLower();

            return query.Where(x =>
                x.Student.Fullname.ToLower().Contains(search) ||
                x.Course.Coursename.ToLower().Contains(search) ||
                x.Status.ToLower().Contains(search));
        }

        public static IQueryable<Enrollment> Sort(
            this IQueryable<Enrollment> query,
            QueryParameters request)
        {
            return request.Sort switch
            {
                "studentName" => query.OrderBy(
                    x => x.Student.Fullname),

                "-studentName" => query.OrderByDescending(
                    x => x.Student.Fullname),

                "courseName" => query.OrderBy(
                    x => x.Course.Coursename),

                "-courseName" => query.OrderByDescending(
                    x => x.Course.Coursename),

                "enrollDate" => query.OrderBy(
                    x => x.Enrolldate),

                "-enrollDate" => query.OrderByDescending(
                    x => x.Enrolldate),

                "status" => query.OrderBy(
                    x => x.Status),

                "-status" => query.OrderByDescending(
                    x => x.Status),

                _ => query.OrderBy(x => x.Enrollmentid)
            };
        }

        public static IQueryable<Enrollment> Paging(
            this IQueryable<Enrollment> query,
            QueryParameters request)
        {
            return query.Skip((request.Page - 1) * request.Size)
                        .Take(request.Size);
        }

        
    }
}