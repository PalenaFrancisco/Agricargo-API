

using Agricargo.Domain.Entities;
using System.Security.Claims;

namespace Agricargo.Application.Interfaces;

public interface IFavoriteService
{
    public void AddFavorite(ClaimsPrincipal user, int tripId);

    public void DeleteFavorite(ClaimsPrincipal user, int id);

    public List<Favorite> GetFavorites(ClaimsPrincipal user);
}
