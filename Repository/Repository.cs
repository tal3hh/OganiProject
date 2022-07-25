using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OganiProject.Context;
using OganiProject.Entities;
using Microsoft.EntityFrameworkCore;
using OganiProject.Repository;

namespace EndProjectOrgani.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task<List<T>> GetAllOrderByAsync(Expression<Func<T, int>> exp, bool AscORDesc = true)
        {
            if (AscORDesc)
            {
                return await _context.Set<T>().AsNoTracking().Where(x => x.Status != DataStatus.Deleted!).OrderBy(exp).ToListAsync();
            }
            else
            {
                return await _context.Set<T>().AsNoTracking().Where(x => x.Status != DataStatus.Deleted!).OrderByDescending(exp).ToListAsync();
            }
        }

        public async Task<List<T>> GetAllFilterAsync(Expression<Func<T, bool>> exp)
        {
            return await _context.Set<T>().AsNoTracking().Where(exp).ToListAsync();
        }

        public async Task<T> FindAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> exp)
        {
            return await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(exp);
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable GetQueryable()
        {
            return _context.Set<T>().AsQueryable();
        }



    }
}
