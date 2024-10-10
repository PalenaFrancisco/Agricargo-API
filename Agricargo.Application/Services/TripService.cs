using Agricargo.Application.Interfaces;
using Agricargo.Application.Models.Requests;
using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;

namespace Agricargo.Application.Services
{
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
                DepartureDate = tripService.DepartureDate,
                ArriveDate = tripService.ArrivalDate,
                Destiny = tripService.Destination,
                Origin = tripService.Origin,
                Price = tripService.Price,
                TripState = tripService.TripState,
                IsFullCapacity = tripService.IsFullCapacity,
                ShipId = tripService.ShipId,
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
    }
}
