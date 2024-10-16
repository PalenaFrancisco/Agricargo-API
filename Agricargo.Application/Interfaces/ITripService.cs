
using Agricargo.Application.Models.Requests;
using Agricargo.Domain.Entities;

namespace Agricargo.Application.Interfaces;

public interface ITripService
{
    public Trip Get(int id);

    public List<Trip> Get();


    public void Delete(int id);


    public void Add(TripCreateRequest tripRequest);

    public void Update(int id, TripCreateRequest tripRequest);
}
