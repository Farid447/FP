using FP.Core.Entities;
using FP.Core.Repositories;
using FP.DAL.Context;

namespace FP.DAL.Repositories;

public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(FPDbContext context) : base(context)
    {
        
    }
}
