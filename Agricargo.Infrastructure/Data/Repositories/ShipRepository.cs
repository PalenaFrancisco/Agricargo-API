using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;
using Agricargo.Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;


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
            .Include(s => s.Trips)
            .Where(s => s.CompanyId == companyId)
            .ToList();
    }

    public Ship GetCompanyShip(int shipId)
    {
        return _context.Ships
            .Include(s => s.Trips)
            .FirstOrDefault(s => s.ShipId == shipId);
    }

}
