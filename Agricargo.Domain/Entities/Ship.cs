

namespace Agricargo.Domain.Entities;

public class Ship
{
    public int ShipId { get; set; }
    public string? TypeShip { get; set; }
    public float Capacity { get; set; }
    public string? Captain { get; set; }
    public bool Available { get; set; } = true;
}
