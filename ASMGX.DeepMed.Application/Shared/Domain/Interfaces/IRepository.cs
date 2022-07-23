using ASMGX.DeepMed.Shared.EntityFramework.Interfaces;
namespace ASMGX.DeepMed.Application.Shared.Domain.Interfaces
{
    public interface IRepository<T>: IReadOnlyRepository<T> where T : IBaseEntity
    {
        void Add(T entity);
        void Edit(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task AddAsync(T entity);
        void EditRange(IEnumerable<T> entities);
        Task AddRangeAsync(IEnumerable<T> entities);
    }
}
