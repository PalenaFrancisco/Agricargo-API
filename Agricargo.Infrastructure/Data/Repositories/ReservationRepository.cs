
using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace Agricargo.Infrastructure.Data.Repositories;

public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
{
    private readonly ApplicationDbContext _context;
    public ReservationRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public List<Reservation> GetReservationsByClientId(Guid clientId)
    {
        return _context.Reservations
       .Include(r => r.Trip) 
        .ThenInclude(t => t.Ship) 
        .Where(c => c.ClientId == clientId)
        .ToList();
    }

    public List<Reservation> GetReservationsByCompanyId(Guid companyId)
    {
        var reservations = _context.Reservations
            .Include(r => r.Trip) 
            .ThenInclude(t => t.Ship)
            .Where(r => r.Trip.Ship.CompanyId == companyId)
            .ToList();

        return reservations;
    }

}
