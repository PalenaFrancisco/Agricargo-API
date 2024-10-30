

using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Agricargo.Infrastructure.Data.Repositories;

public class FavoriteRepository : BaseRepository<Favorite>, IFavoriteRepository
{
    private readonly ApplicationDbContext _context;
    public FavoriteRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public List<Favorite> GetClientFavorites(Guid clientId)
    {
        return _context.Favorites
            .Include(f => f.Trip)
            .Where(f => f.ClientId == clientId)
            .ToList();
    }

    public Favorite GetFavoriteById(int favoriteId)
    {
        return _context.Favorites
            .Include(f => f.Trip) 
            .FirstOrDefault(f => f.Id == favoriteId);
    }

    public Favorite GetFavoriteByClientAndTrip(Guid clientId, int tripId)
    {
        return _context.Favorites
            .FirstOrDefault(f => f.ClientId == clientId && f.TripId == tripId);
    }
}
