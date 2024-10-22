
namespace Agricargo.Domain.Entities;

public class Favorite
{
    public int Id { get; set; }

    public int TripId { get; set; }
    public Trip Trip { get; set; }

    public Guid ClientId { get; set; }
    public Client Client { get; set; }
}
