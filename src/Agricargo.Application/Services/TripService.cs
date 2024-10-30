

using Agricargo.Application.Interfaces;
using Agricargo.Application.Models.DTOs;
using Agricargo.Application.Models.Requests;
using Agricargo.Application.Models.RequestsM;
using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;
using Agricargo.Infrastructure.Data.Repositories;
using System.Security.Claims;

namespace Agricargo.Application.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;
    private readonly IShipService _shipService;
    private readonly IReservationRepository _reservationRepository;
    private readonly IFavoriteRepository _favoriteRepository;
    public TripService(ITripRepository tripRepository, IShipService shipService, IReservationRepository reservationRepository, IFavoriteRepository favoriteRepository)
    {
        _tripRepository = tripRepository;
        _shipService = shipService;
        _reservationRepository = reservationRepository;
        _favoriteRepository = favoriteRepository;
    }

    private Guid GetCompanyIdFromUser(ClaimsPrincipal user)
    {
        var userId = user.FindFirst("id")?.Value;

        if (!Guid.TryParse(userId, out Guid parsedGuid))
        {
            throw new UnauthorizedAccessException("Token inválido");
        }

        return parsedGuid;
    }

    public void Add(TripCreateRequest tripService, ClaimsPrincipal user)
    {
        var userId = GetCompanyIdFromUser(user);

        bool isShipOwned = _shipService.IsShipOwnedByCompany(tripService.ShipId, userId);

        Ship ship = _shipService.Get(user, tripService.ShipId);

        if (!isShipOwned)
        {
            throw new UnauthorizedAccessException("Barco no asociado a la empresa");
        }

        foreach (var trip in ship.Trips)
        {
            if ((tripService.DepartureDate <= trip.ArriveDate) && (tripService.ArriveDate >= trip.DepartureDate))
            {
                throw new Exception("El barco ya tiene un viaje para esa fecha");
            }
        }

        _tripRepository.Add(new Trip
        {
            Origin = tripService.Origin,
            Destination = tripService.Destination,
            DepartureDate = tripService.DepartureDate,
            ArriveDate = tripService.ArriveDate,
            Price = tripService.Price,
            ShipId = tripService.ShipId,
            AvailableCapacity = ship.Capacity
        });
    }

    public void Delete(int id, ClaimsPrincipal user)
    {
        var trip = _tripRepository.Get(id);

        if (trip == null)
        {
            throw new Exception("Viaje no encontrado");
        }
        var trips = GetTrips(user);
        if (!trips.Any(t => t.Id == id))
        {
            throw new UnauthorizedAccessException("No tienes permiso de eliminar este viaje");
        }

        if (_reservationRepository.TripHasAReservation(id))
        {
            throw new Exception("Este viaje tiene una reserva no se puede borrar");
        }

        var favorites = _favoriteRepository.Get().Where(f => f.TripId == id);

        if (favorites is not null)
        {
            foreach (var favorite in favorites)
            {
                _favoriteRepository.Delete(favorite);
            }
        }

        _tripRepository.Delete(trip);

    }

    public Trip Get(int id)
    {
        var trip = _tripRepository.Get(id);

        return trip;
    }

    public TripDTO GetToDto(ClaimsPrincipal user, int id)
    {
        var trip = _tripRepository.Get(id);
        var companyId = GetCompanyIdFromUser(user);

        if (trip.Ship.CompanyId != companyId)
        {
            throw new UnauthorizedAccessException("No esta habilitado a obtener este viaje");
        }

        if (trip == null)
        {
            throw new Exception("No se encontro un viaje");
        }

        return new TripDTO
        {
            Id = trip.Id,
            Origin = trip.Origin,
            Destination = trip.Destination,
            PricePerTon = trip.Price,
            DepartureDate = trip.DepartureDate,
            ArriveDate = trip.ArriveDate,
            Capacity = trip.AvailableCapacity
        };
    }

    public List<TripDTO> Get(TripSearchRequest tripSearch, out string searchType)
    {
        var exactGet = _tripRepository.Get()
            .Where(t => t.Origin == tripSearch.Origin
                        && t.Destination == tripSearch.Destination
                        && tripSearch.GrainAmount <= t.AvailableCapacity)
            .Select(t => new TripDTO
            {
                Id = t.Id,
                Origin = t.Origin,
                Destination = t.Destination,
                PricePerTon = t.Price,
                DepartureDate = t.DepartureDate,
                ArriveDate = t.ArriveDate,
                Capacity = t.AvailableCapacity
            })
            .ToList();

        if (exactGet.Any())
        {
            searchType = "ExactSearch";
            return exactGet;
        }

        var partialGet = _tripRepository.Get()
            .Where(t => (t.Origin == tripSearch.Origin || t.Destination == tripSearch.Destination)
                        && tripSearch.GrainAmount <= t.AvailableCapacity)
            .Select(t => new TripDTO
            {
                Id = t.Id,
                Origin = t.Origin,
                Destination = t.Destination,
                PricePerTon = t.Price,
                DepartureDate = t.DepartureDate,
                ArriveDate = t.ArriveDate,
                Capacity = t.AvailableCapacity
            })
            .ToList();

        searchType = "PartialSearch";
        return partialGet;
    }


    public void Update(int id, TripUpdateRequest tripRequest, ClaimsPrincipal user)
    {
        var trip = _tripRepository.Get(id);
        if (trip == null)
        {
            throw new Exception("Viaje no encontrado");
        }
        var trips = GetTrips(user);

        if (!trips.Any(t => t.Id == id))
        {
            throw new UnauthorizedAccessException("No tienes permiso a realizar esta accion");
        }

        if (trip.Ship.Trips.Count() > 1)
        {
            foreach (var tripOfShip in trip.Ship.Trips)
            {
                if ((tripRequest.DepartureDate <= tripOfShip.ArriveDate) && (tripRequest.ArriveDate >= tripOfShip.DepartureDate))
                {
                    throw new Exception("El barco ya tiene un viaje para esa fecha");
                }
            }
        }

        if (trip.TripState == "En curso")
        {
            throw new Exception("No se puede modificar el viaje porque está en curso.");
        }

        if (_reservationRepository.TripHasAReservation(id))
        {
            throw new Exception("Este viaje tiene una reserva no se puede modificar");
        }

        trip.Origin = tripRequest.Origin ?? trip.Origin;
        trip.Destination = tripRequest.Destination ?? trip.Destination;
        trip.DepartureDate = tripRequest.DepartureDate;
        trip.ArriveDate = tripRequest.ArriveDate;
        trip.Price = tripRequest.Price != 0 ? tripRequest.Price : trip.Price;

        _tripRepository.Update(trip);

    }

    public List<TripDTO> GetTrips(ClaimsPrincipal user)
    {
        var userId = GetCompanyIdFromUser(user);

        var trips = _tripRepository.GetCompanyTrips(userId);

        if (trips == null || !trips.Any())
        {
            throw new Exception("No se encontraron viajes para la empresa.");
        }

        foreach (var trip in trips)
        {
            var currentDate = DateTime.Now;

            // Actualizar el estado del viaje según las fechas
            if (currentDate < trip.DepartureDate)
            {
                trip.TripState = "Pendiente";  // El viaje aún no ha comenzado
            }
            else if (currentDate >= trip.DepartureDate && currentDate <= trip.ArriveDate)
            {
                trip.TripState = "En curso";  // El viaje está en progreso
            }
            else if (currentDate > trip.ArriveDate)
            {
                trip.TripState = "Completado";  // El viaje ha finalizado
            }

            // Actualizar el viaje con el nuevo estado
            _tripRepository.Update(trip);
        }

        return trips.Select(t => new TripDTO
        {
            Id = t.Id,
            Origin = t.Origin,
            Destination = t.Destination,
            PricePerTon = t.Price,
            DepartureDate = t.DepartureDate,
            ArriveDate = t.ArriveDate,
            Capacity = t.Ship.Capacity
        }).ToList();
    }

    public List<Trip> GetTripsOfShips(ClaimsPrincipal user, int id)
    {
        var companyId = GetCompanyIdFromUser(user);
        GetTrips(user);
        if (_shipService.IsShipOwnedByCompany(id, companyId))
        {
            var trips = _tripRepository.GetTripsOfShip(id);
            if (trips == null)
            {
                throw new Exception("El barco no tiene viajes");
            }

            return trips;
        }
        else
        {
            throw new UnauthorizedAccessException("No tiene permitido obtener esta informacion del barco");
        }
    }
}
