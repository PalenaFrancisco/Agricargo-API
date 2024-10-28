

using Agricargo.Domain.Entities;
using System.Security.Claims;
using Agricargo.Application.Models.DTOs;

namespace Agricargo.Application.Interfaces;

public interface IFavoriteService
{
    public void AddFavorite(ClaimsPrincipal user, int tripId);

    public void DeleteFavorite(ClaimsPrincipal user, int id);

    public List<FavoriteDTO> GetFavorites(ClaimsPrincipal user);
}
