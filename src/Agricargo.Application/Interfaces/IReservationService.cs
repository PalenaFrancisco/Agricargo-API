
using Agricargo.Application.Models.DTOs;
using Agricargo.Application.Models.Requests;
using Agricargo.Domain.Entities;
using System.Security.Claims;

namespace Agricargo.Application.Interfaces;

public interface IReservationService
{
    public void AddReservation(ClaimsPrincipal user, int tripId, float amountReserved);

    public List<ReservationDTO> GetClientReservations(ClaimsPrincipal user);

    public List<Reservation> GetCompanyReservations(ClaimsPrincipal user);

    public void DeleteReservation(int reservationId, ClaimsPrincipal user);
}
