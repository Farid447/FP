using FP.Core.Entities.Common;
using System.Linq.Expressions;

namespace FP.Core.Repositories;

public interface IGenericRepository<T> where T : BaseEntity, new()
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> expression);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task DeleteAsync(T entity);
    Task ToggleAsync(int id);
    Task SaveAsync();
}
