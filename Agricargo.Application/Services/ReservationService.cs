

using Agricargo.Application.Interfaces;
using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;
using Agricargo.Infrastructure.Data.Repositories;
using System.Security.Claims;

namespace Agricargo.Application.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ITripService _tripService;

    public ReservationService(IReservationRepository reservationRepository, ITripService tripService)
    {
        _reservationRepository = reservationRepository;
        _tripService = tripService;
    }

    public void AddReservation(ClaimsPrincipal user, int tripId)
    {
        var trip = _tripService.Get(tripId);

        if(trip is null || trip.IsFullCapacity)
        {
            throw new Exception("Viaje no encontrado");
        }

        var currentDate = DateTime.Now;
        if (trip.DepartureDate <= currentDate)
        {
            throw new Exception("Viaje no disponible");
        }

        var userIdClaim = user.FindFirst("id")?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new Exception("Usuario no válido.");
        }

        Guid clientId;
        if (!Guid.TryParse(userIdClaim, out clientId))
        {
            throw new Exception("El ID de usuario no es un Guid válido.");
        }

        var reservation = new Reservation
        {
            ClientId = clientId,
            TripId = tripId,
            PurchaseAmount = trip.Price,
            PurchaseDate = currentDate,
            DepartureDate = trip.DepartureDate,
            ArriveDate = trip.ArriveDate,
            ReservationStatus = trip.TripState 
        };

        _reservationRepository.Add(reservation);
    }

    public List<Reservation> GetReservations(ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst("id")?.Value;

        if (userIdClaim == null)
        {
            throw new Exception("Usuario no autenticado.");
        }

        Guid clientId = Guid.Parse(userIdClaim);
        return _reservationRepository.GetReservationsByClientId(clientId);
    }

    
}
