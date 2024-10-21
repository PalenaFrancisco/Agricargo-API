

using Agricargo.Application.Models.Requests;
using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;
using System.Security.Claims;

namespace Agricargo.Application.Services;

public class ShipService : IShipService
{
    private readonly IShipRepository _shipRepository;

    public ShipService(IShipRepository shipRepository)
    {
        _shipRepository = shipRepository;
    }

    public Ship Get(int id)
    {
        var ship = _shipRepository.Get(id);
        return ship;
    }
    public List<Ship> Get()
    {
        return _shipRepository.Get();
    }

    public void Delete(Ship ship)
    {
        _shipRepository.Delete(ship);
    

    }

    public void Add(ShipCreateRequest shipService, ClaimsPrincipal user)
    {
        _shipRepository.Add(new Ship
        {
            TypeShip = shipService.TypeShip,
            Capacity = shipService.Capacity,
            Captain = shipService.Captain,
            Available = shipService.Available,
            CompanyId = GetIdFromUser(user)
        });
    }

    //------------------------------------------------------

    public bool IsShipOwnedByCompany(int shipId, Guid companyId) 
    {
        var ship = _shipRepository.Get(shipId);

        if (ship == null) 
        {
            return false;
        }
        return ship.CompanyId == companyId;
    }

    private Guid GetIdFromUser(ClaimsPrincipal user)
    {
        var userId = user.FindFirst("id")?.Value;

        if (!Guid.TryParse(userId, out Guid parsedGuid))
        {
            throw new UnauthorizedAccessException("Token inválido");
        }

        return parsedGuid;
    }

}
