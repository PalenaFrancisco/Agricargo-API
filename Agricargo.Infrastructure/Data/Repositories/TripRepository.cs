

using Agricargo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Agricargo.Infrastructure.Data.Repositories;

public class TripRepository : BaseRepository<Trip>, ITripRepository
{
    private readonly ApplicationDbContext _context;
    public TripRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public List<Trip> GetCompanyTrips(Guid companyId)
    {
        return _context.Trips
        .Where(t => t.Ship.CompanyId == companyId)
        .ToList();
    }

    public List<Trip> GetTripsOfShip(int id)
    {
        return _context.Trips
            .Where(t => t.ShipId == id)
            .ToList();
    }
}
