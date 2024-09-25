using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;


namespace Agricargo.Infrastructure.Repositories;

public class ShipRepository : IShipRepository
{
    private static readonly List<Ship> _ships = new List<Ship>();
    private int _id = 0;
    public void Add(Ship ship)
    {
        ship.ShipId = _id++;
        _ships.Add(ship);
    }

    public bool Delete(int id)
    {
        var shipSelected = Get(id);
        if (shipSelected != null) 
        { 
            _ships.Remove(shipSelected);
            return true;
        }
        return false;
    }

    public Ship Get(int id)
    {
        return _ships.FirstOrDefault(ship => ship.ShipId == id);
    }

    public void Update(int id)
    {
        var shipSelected = Get(id);
        if (shipSelected != null)
        {
            
        }
    }
}
