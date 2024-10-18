
using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;
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
       .Where(c => c.ClientId == clientId)
       .ToList();
    }
}
