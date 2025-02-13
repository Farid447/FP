using FP.Core.Entities;

namespace FP.Core.Repositories;

public interface IStadiumRepository : IGenericRepository<Stadium>
{
    Task<Stadium> GetByNameAsync(string name);
}
