

using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;

namespace Agricargo.Application.Services;

public class ShipABM
{
    private readonly IShipRepository _shipRepository;

    public ShipABM(IShipRepository shipRepository)
    {
        _shipRepository = shipRepository;
    }

    public Ship Get(int id)
    {
        var ship = _shipRepository.Get(id);
        return ship;
    }

    public string Delete(int id)
    {
        var isDeleted = _shipRepository.Delete(id);
        if (isDeleted)
        {
            return "Borrado";
        }
        return "No se borró";
        
    }

    public void Add(string type, float capacity, string captain, bool isAvailable)
    {
        _shipRepository.Add(new Ship
        {
            TypeShip = type,
            Capacity = capacity,
            Captain = captain,
            Available = isAvailable
        });
    }
}
