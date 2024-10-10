using Agricargo.Application.Models.Requests;
using Agricargo.Domain.Entities;

namespace Agricargo.Application.Interfaces
{
    public interface ITripService
    {
        public Trip Get(int id);
        public List<Trip> Get();
        public void Delete(Trip trip);
        public void Add(TripCreateRequest tripService);
    }
}
