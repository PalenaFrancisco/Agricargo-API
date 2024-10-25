
using Agricargo.Application.Models.DTOs;
using Agricargo.Application.Models.Requests;
using Agricargo.Application.Models.RequestsM;
using Agricargo.Domain.Entities;
using System.Security.Claims;

namespace Agricargo.Application.Interfaces;

public interface ITripService
{
    public Trip Get(int id);

    public List<TripDTO> Get(TripSearchRequest tripSearch);

    public void Delete(int id, ClaimsPrincipal user);

    public void Add(TripCreateRequest tripRequest, ClaimsPrincipal user);

    public void Update(int id, TripUpdateRequest tripRequest, ClaimsPrincipal user);

    public List<TripDTO> GetTrips(ClaimsPrincipal user);

    public List<Trip> GetTripsOfShips(ClaimsPrincipal user, int id);

}
