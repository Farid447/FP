using FP.Core.Entities;
using FP.Core.Repositories;
using FP.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace FP.DAL.Repositories;

public class StadiumRepository(FPDbContext context) : GenericRepository<Stadium>(context), IStadiumRepository
{
    public async Task<Stadium> GetByNameAsync(string name)
    {
        return await context.Stadiums.FirstOrDefaultAsync(x => x.Name == name);
    }
}
