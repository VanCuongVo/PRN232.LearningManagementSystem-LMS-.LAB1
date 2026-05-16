using PRN232.LMS.Models.Entities;
using PRN232.LMS.Repositories.Data;
using PRN232.LMS.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.LMS.Repositories.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LmsdbContext _context;

        public IGenericRepositories<Student> Students { get; }

        public UnitOfWork(LmsdbContext context, IGenericRepositories<Student> students)
        {
            _context = context;
            Students = students;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
