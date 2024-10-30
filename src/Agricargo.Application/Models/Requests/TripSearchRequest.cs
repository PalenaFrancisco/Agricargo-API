

namespace Agricargo.Application.Models.RequestsM;

public class TripSearchRequest
{
    public string? Origin { get; set; }
    public string? Destination { get; set; }
    public float GrainAmount { get; set; }
}
