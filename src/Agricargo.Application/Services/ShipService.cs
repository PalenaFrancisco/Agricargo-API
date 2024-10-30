

using Agricargo.Application.Models.Requests;
using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;
using System.Security.Claims;
using Agricargo.Application.Models.DTOs;
using Agricargo.Application.Interfaces;
using Agricargo.Infrastructure.Data.Repositories;

namespace Agricargo.Application.Services;

public class ShipService : IShipService
{
    private readonly IShipRepository _shipRepository;
    private readonly ITripRepository _tripRepository;
    private readonly IReservationRepository _reservationRepository;

    public ShipService(IShipRepository shipRepository, ITripRepository tripRepository, IReservationRepository reservationRepository)
    {
        _shipRepository = shipRepository;
        _tripRepository = tripRepository;
        _reservationRepository = reservationRepository;
    }

    public Ship Get(ClaimsPrincipal user, int id)
    {
        var ship = _shipRepository.GetCompanyShip(id);

        if (ship is null)
        {
            throw new Exception("El barco no existe");
        }

        var userId = GetIdFromUser(user);

        if (!IsShipOwnedByCompany(ship.Id, userId))
        {
            throw new Exception("No está habilitado para obtener ese barco");
        }

        return ship;
    }

    public ShipDTO GetToDto(ClaimsPrincipal user, int id)
    {
        var ship = _shipRepository.GetCompanyShip(id);

        if (ship is null)
        {
            throw new Exception("El barco no existe");
        }

        var userId = GetIdFromUser(user);

        if (!IsShipOwnedByCompany(ship.Id, userId))
        {
            throw new UnauthorizedAccessException("No está habilitado para obtener ese barco");
        }

        ShipDTO shipDto = new ShipDTO
        {
            Id = ship.Id,
            TypeShip = ship.TypeShip,
            Capacity = ship.Capacity,
            Captain = ship.Captain,
            ShipPlate = ship.ShipPlate,
        };

        return shipDto;
    }

    public List<ShipDTO> Get(ClaimsPrincipal user)
    {
        var userId = GetIdFromUser(user);
        var ships = _shipRepository.GetCompanyShips(userId);

        if (ships != null) 
        {
            return ships.Select(ship => new ShipDTO
                {
                    Id = ship.Id,
                    TypeShip = ship.TypeShip,
                    Captain = ship.Captain,
                    Capacity = ship.Capacity,
                    ShipPlate = ship.ShipPlate,
                }).ToList();
        }

        throw new Exception("No se encontraron barcos");

    }

    public void Delete(ClaimsPrincipal user, int id)
    {
        var ship = _shipRepository.Get(id);

        if (ship is null)
        {
            throw new Exception("El barco no existe");
        }

        var userId = GetIdFromUser(user);

        if (!IsShipOwnedByCompany(ship.Id, userId))
        {
            throw new UnauthorizedAccessException("No está habilitado para borrar ese barco");
        }

        if (ship.Trips != null) 
        {
            
            foreach (var trip in ship.Trips)
            {
                if (_reservationRepository.TripHasAReservation(trip.Id)) 
                {
                    throw new Exception($"El viaje {trip.Id} no se puede borrar porque tiene una reserva. Accion Cancelada");
                }
                    _tripRepository.Delete(trip);
            }
        }

        _shipRepository.Delete(ship);
    }

    public void Add(ShipCreateRequest shipService, ClaimsPrincipal user)
    {
        var ships = _shipRepository.Get();

        var exisitingShip = ships.FirstOrDefault(s => s.ShipPlate == shipService.ShipPlate);

        if (exisitingShip != null) 
        {
            throw new Exception("Ya existe un barco con la misma patente.");
        }

        _shipRepository.Add(new Ship
        {
            TypeShip = shipService.TypeShip,
            Capacity = shipService.Capacity,
            Captain = shipService.Captain,
            ShipPlate = shipService.ShipPlate,
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

        var ships = _shipRepository.Get();

        var exisitingShip = ships.FirstOrDefault(s => s.ShipPlate == shipRequest.ShipPlate);


        if (exisitingShip != null) 
        {
            throw new Exception("Ya existe un barco con la misma patente.");
        }

        if (ship.CompanyId != userId)
        {
            throw new UnauthorizedAccessException("No tienes permiso para modificar este barco");
        }
        ship.TypeShip = shipRequest.TypeShip ?? ship.TypeShip;
        ship.Capacity = shipRequest.Capacity != 0 ? shipRequest.Capacity : ship.Capacity;
        ship.Captain = shipRequest.Captain ?? ship.Captain;
        ship.ShipPlate = shipRequest.ShipPlate ?? ship.ShipPlate;
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
