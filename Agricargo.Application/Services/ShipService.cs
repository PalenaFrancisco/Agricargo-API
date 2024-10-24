

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

    public Ship Get(ClaimsPrincipal user, int id)
    {
        var ship = _shipRepository.GetCompanyShip(id);

        if (ship is null)
        {
            throw new Exception("El barco no existe");
        }

        var userId = GetIdFromUser(user);

        if (!IsShipOwnedByCompany(ship.ShipId, userId))
        {
            throw new Exception("No está habilitado para obtener ese barco");
        }

        return ship;
    }
    public List<Ship> Get(ClaimsPrincipal user)
    {
        var userId = GetIdFromUser(user);
        return _shipRepository.GetCompanyShips(userId);
    }

    public void Delete(ClaimsPrincipal user, int id)
    {
        var ship = _shipRepository.Get(id);

        if (ship is null)
        {
            throw new Exception("El barco no existe");
        }

        var userId = GetIdFromUser(user);

        if (!IsShipOwnedByCompany(ship.ShipId, userId))
        {
            throw new Exception("No está habilitado para borrar ese barco");
        }

        _shipRepository.Delete(ship);
    }

    public void Add(ShipCreateRequest shipService, ClaimsPrincipal user)
    {
        _shipRepository.Add(new Ship
        {
            TypeShip = shipService.TypeShip,
            Capacity = shipService.Capacity,
            Captain = shipService.Captain,
            //Available = shipService.Available,
            CompanyId = GetIdFromUser(user)
        });
    }

    public void Update(ClaimsPrincipal user, int id, ShipCreateRequest shipRequest)
    {
        var ship = _shipRepository.Get(id);

        if (ship is null)
        {
            throw new Exception("El barco no existe");
        }

        var userId = GetIdFromUser(user);

        if (ship.CompanyId != userId)
        {
            throw new UnauthorizedAccessException("No tienes permiso para modificar este barco");
        }
        ship.TypeShip = shipRequest.TypeShip ?? ship.TypeShip;
        ship.Capacity = shipRequest.Capacity != 0 ? shipRequest.Capacity : ship.Capacity;
        ship.Captain = shipRequest.Captain ?? ship.Captain;
        //ship.Available = shipRequest.Available;

        _shipRepository.Update(ship);
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
