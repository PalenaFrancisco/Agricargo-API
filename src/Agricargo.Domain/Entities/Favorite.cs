
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Agricargo.Domain.Entities;

public class Favorite
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int TripId { get; set; }
    public Trip Trip { get; set; }

    [Required]
    public Guid ClientId { get; set; }
    public Client Client { get; set; }
}
