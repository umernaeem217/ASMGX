using ASMGX.DeepMed.Shared.EntityFramework.Interfaces;
using System.Linq.Expressions;

namespace ASMGX.DeepMed.Application.Shared.Domain.Interfaces
{
    public interface IRepository<T> where T : IBaseEntity
    {
        T? GetById(int id);
        IEnumerable<T> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void SaveChanges();
        Task SaveChangesAsync();
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
    }
}
