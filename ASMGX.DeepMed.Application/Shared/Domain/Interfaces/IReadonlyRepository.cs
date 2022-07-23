using ASMGX.DeepMed.Shared.EntityFramework.Interfaces;
using System.Linq.Expressions;

namespace ASMGX.DeepMed.Application.Shared.Domain.Interfaces
{
    public interface IReadOnlyRepository<T> where T : IBaseEntity
    {
        T? GetById(string id);
        IEnumerable<T> GetAll();
        IQueryable<T> GetIQueryable();
        IQueryable<T> Find(Expression<Func<T, bool>> expression);
        void SaveChanges();
        Task SaveChangesAsync();
        Task<T?> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
