using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;


namespace Agricargo.Infrastructure.Data.Repositories;

public class ShipRepository : BaseRepository<Ship>, IShipRepository
{
    private readonly ApplicationDbContext _context;

    public ShipRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public List<Ship> GetCompanyShips(Guid companyId)
    {
        return _context.Ships
            .Where(s => s.CompanyId == companyId)
            .ToList();
    }
}
