using PRN232.LMS.Models.Entities;

namespace PRN232.LMS.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IGenericRepository<Student> Students { get; }

    }
}
