
using Agricargo.Application.Models.Requests;
using Agricargo.Domain.Entities;
using System.Security.Claims;

namespace Agricargo.Application.Interfaces;

public interface IReservationService
{
    public void AddReservation(ClaimsPrincipal user, int tripId);

    public List<Reservation> GetReservations(ClaimsPrincipal user);
}
