using FP.Core.Entities.Common;

namespace FP.Core.Repositories;

public interface IGenericRepository<T> where T : BaseEntity, new()
{
    Task GetAll();
    Task AddAsync(T entity);
    void Delete(T entity);
    Task DeleteAsync(int id);
}
