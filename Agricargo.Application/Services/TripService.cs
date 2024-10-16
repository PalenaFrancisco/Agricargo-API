

using Agricargo.Application.Interfaces;
using Agricargo.Application.Models.Requests;
using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;
using Agricargo.Infrastructure.Data.Repositories;

namespace Agricargo.Application.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;

    public TripService(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }
    public void Add(TripCreateRequest tripService)
    {
        _tripRepository.Add(new Trip
        {
            Origin = tripService.Origin,
            Destiny = tripService.Destiny,
            DepartureDate = tripService.DepartureDate,
            ArriveDate = tripService.ArriveDate,
            Price = tripService.Price,
            ShipId = tripService.ShipId
        });
    }

    public void Delete(Trip trip)
    {
        _tripRepository.Delete(trip);
    }

    public Trip Get(int id)
    {
        var trip = _tripRepository.Get(id);
        return trip;
    }

    public List<Trip> Get()
    {
        return _tripRepository.Get();
    }

    public void Update(int id, TripCreateRequest tripRequest)
    {
        var trip = _tripRepository.Get(id);
        if (trip != null)
        {
            trip.Origin = tripRequest.Origin;
            trip.Destiny = tripRequest.Destiny;
            trip.DepartureDate = tripRequest.DepartureDate;
            trip.ArriveDate = tripRequest.ArriveDate;
            trip.Price = tripRequest.Price;

            _tripRepository.Update(trip);
        } else
        {
            throw new Exception("Viaje no encontrado");
        }
    }
}
