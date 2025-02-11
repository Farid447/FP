using FP.Core.Entities;
using FP.Core.Repositories;
using FP.DAL.Context;

namespace FP.DAL.Repositories;

public class CategoryRepository : GenericRepository<Stadium>, ICategoryRepository
{
    public CategoryRepository(FPDbContext context) : base(context)
    {
    }
}
