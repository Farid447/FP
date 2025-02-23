using FP.Core.Entities;
using FP.Core.Repositories;
using FP.DAL.Context;

namespace FP.DAL.Repositories;

public class RatingRepository : GenericRepository<Rating>, IRatingRepository
{
    public RatingRepository(FPDbContext context) : base(context)
    {

    }
}