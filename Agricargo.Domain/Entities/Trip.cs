

namespace Agricargo.Domain.Entities;

public class Trip
{
    public int Id { get; set; }
    public string? Origin { get; set; }
    public string? Destiny { get; set; }
    public float Price { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime ArriveDate { get; set; }
    public string? TripState { get; set; }
    public bool IsFullCapacity { get; set; } = false;
}
