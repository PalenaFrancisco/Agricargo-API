

using Agricargo.Application.Interfaces;
using Agricargo.Application.Models.DTOs;
using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;
using Agricargo.Infrastructure.Data.Repositories;
using System.Security.Claims;

namespace Agricargo.Application.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ITripRepository _tripRepository;
    private readonly ITripService _tripService;
    private readonly IShipService _shipService;

    public ReservationService(IReservationRepository reservationRepository, ITripService tripService, IShipService shipService, ITripRepository tripRepository)
    {
        _reservationRepository = reservationRepository;
        _tripService = tripService;
        _shipService = shipService;
        _tripRepository = tripRepository;
    }

    public void AddReservation(ClaimsPrincipal user, int tripId, float amountReserved)
    {
        var trip = _tripService.Get(tripId);

        if (trip is null)
        {
            throw new Exception("Viaje no encontrado.");
        }

        if ((trip.AvailableCapacity - amountReserved) < 0)
        {
            throw new Exception("Este viaje no tiene la capacidad suficiente para transportar su carga.");
        }

        var currentDate = DateTime.Now;
        if (trip.DepartureDate <= currentDate)
        {
            throw new Exception("Viaje no disponible.");
        }

        if ((trip.DepartureDate - currentDate).TotalDays < 2)
        {
            throw new Exception("La reserva debe realizarse al menos dos días antes de la fecha de salida del viaje.");
        }

        var clientId = GetIdFromUser(user);

        var reservation = new Reservation
        {
            ClientId = clientId,
            TripId = tripId,
            PurchasePrice = trip.Price * amountReserved,
            PurchaseAmount = amountReserved,
            PurchaseDate = currentDate,
            DepartureDate = trip.DepartureDate,
            ArriveDate = trip.ArriveDate,
            ReservationStatus = trip.TripState
        };

        trip.AvailableCapacity -= amountReserved;

    
        _tripRepository.Update(trip);

        _reservationRepository.Add(reservation);
    }

    public List<ReservationDTO> GetClientReservations(ClaimsPrincipal user)
    {
        var userIdClaim = GetIdFromUser(user);

        var reservations = _reservationRepository.GetReservationsByClientId(userIdClaim);

        // Mapea las reservas a DTOs
        var reservationDtos = reservations.Select(r => new ReservationDTO
        {
            Id = r.Id,
            Trip = $"{r.Trip.Origin} - {r.Trip.Destination}",
            Date = r.PurchaseDate,
            Price = r.PurchasePrice,
            GrainQuantity = r.PurchaseAmount,
            Status = r.ReservationStatus
        }).ToList();

        return reservationDtos;
    }

    public List<Reservation> GetCompanyReservations(ClaimsPrincipal user)
    {
        var userIdClaim = GetIdFromUser(user);

        return _reservationRepository.GetReservationsByCompanyId(userIdClaim);
    }
    public void DeleteReservation(int reservationId, ClaimsPrincipal user)
    {
        var reservation = _reservationRepository.Get(reservationId);

        if (reservation == null)
        {
            throw new Exception("Reserva no encontrada.");
        }

        if (reservation.DepartureDate <= DateTime.Now)
        {
            throw new Exception("No se puede eliminar la reserva porque el viaje ya ha comenzado o finalizado.");
        }

        var userId = GetIdFromUser(user);

        if (reservation.ClientId != userId)
        {
            throw new UnauthorizedAccessException("No esta autorizado a realizar esta accion");
        }

        _reservationRepository.Delete(reservation);

        var trip = _tripService.Get(reservation.TripId);
        bool isShipOwned = _shipService.IsShipOwnedByCompany(trip.ShipId, userId);

        if (trip == null || !isShipOwned)
        {
            throw new Exception("No tienes permiso para eliminar esta reserva.");
        }

        trip.AvailableCapacity += reservation.PurchaseAmount;

    

        _tripRepository.Update(trip);

        _reservationRepository.Delete(reservation);

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
