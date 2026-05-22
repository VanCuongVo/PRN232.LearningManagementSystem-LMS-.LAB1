using PRN232.LMS.Models.Entities;
using PRN232.LMS.Repositories.Data;
using PRN232.LMS.Repositories.IRepositories;

namespace PRN232.LMS.Repositories.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LmsdbContext _context;
        public IStudentRepositories Students { get; }

        public ISubjectRepositories Subjects { get; }

        public ISemestersRepositories Semesters { get; }

        public UnitOfWork(
            LmsdbContext context,
            IStudentRepositories students,
            ISubjectRepositories subjects,
            ISemestersRepositories semesters
            )
        {
            _context = context;
            Students = students;
            Subjects = subjects;
            Semesters = semesters;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
