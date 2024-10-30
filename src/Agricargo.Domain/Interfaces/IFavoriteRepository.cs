

using Agricargo.Domain.Entities;
using Agricargo.Infrastructure.Repositories;

namespace Agricargo.Domain.Interfaces;

public interface IFavoriteRepository : IBaseRepository<Favorite>
{
    public List<Favorite> GetClientFavorites(Guid clientId);
    public Favorite GetFavoriteById(int favoriteId);
    public Favorite GetFavoriteByClientAndTrip(Guid clientId, int tripId);
}
