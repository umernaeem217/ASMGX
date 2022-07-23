using ASMGX.DeepMed.Application.Shared.Domain.Interfaces;
using ASMGX.DeepMed.Infrastructure.Contexts;
using ASMGX.DeepMed.Shared.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ASMGX.DeepMed.Application.Shared.Domain.Concrete
{
    public class ReadOnlyRepository<T>: IReadOnlyRepository<T> where T : class, IBaseEntity
    {
        private readonly ApplicationDbContext _context;

        public ReadOnlyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public T? GetById(string id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetIQueryable()
        {
            return _context.Set<T>();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
