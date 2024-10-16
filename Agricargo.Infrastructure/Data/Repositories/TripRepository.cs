

using Agricargo.Domain.Entities;

namespace Agricargo.Infrastructure.Data.Repositories;

public class TripRepository : BaseRepository<Trip>, ITripRepository
{
    public TripRepository(ApplicationDbContext context) : base(context) { }
}
