using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;


namespace Agricargo.Infrastructure.Data.Repositories;

public class ShipRepository : BaseRepository<Ship>, IShipRepository
{

    public ShipRepository(ApplicationDbContext context) : base(context) { }
}
