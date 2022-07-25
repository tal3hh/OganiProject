using System;
using System.Threading.Tasks;
using OganiProject.Context;
using OganiProject.Entities;
using EndProjectOrgani.Repository;
using OganiProject.Repository;

namespace OganiProject.UniteOfWork
{
    public class Uow : IUow
    {
        private readonly AppDbContext _context;

        public Uow(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(_context);
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
