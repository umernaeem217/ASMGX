using ASMGX.DeepMed.Application.Shared.Domain.Interfaces;
using ASMGX.DeepMed.Infrastructure.Contexts;
using ASMGX.DeepMed.Shared.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ASMGX.DeepMed.Application.Shared.Domain.Concrete
{
    public class Repository<T> : ReadOnlyRepository<T>, IRepository<T> where T : class, IBaseEntity
    {
        private readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public void Edit(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void EditRange(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
    }
}
