

using Agricargo.Application.Models.Requests;
using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;

namespace Agricargo.Application.Services;

public class ShipService : IShipService
{
    private readonly IShipRepository _shipRepository;

    public ShipService(IShipRepository shipRepository)
    {
        _shipRepository = shipRepository;
    }

    public Ship Get(int id)
    {
        var ship = _shipRepository.Get(id);
        return ship;
    }
    public List<Ship> Get()
    {
        return _shipRepository.Get();
    }

    public void Delete(Ship ship)
    {
        _shipRepository.Delete(ship);
    

    }

    public void Add(ShipCreateRequest shipService)
    {
        _shipRepository.Add(new Ship
        {
            TypeShip = shipService.TypeShip,
            Capacity = shipService.Capacity,
            Captain = shipService.Captain,
            Available = shipService.Available,
            CompanyId = shipService.CompanyId
        });
    }

}
