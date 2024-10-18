

using Agricargo.Domain.Entities;
using Agricargo.Infrastructure.Repositories;

namespace Agricargo.Domain.Interfaces;

public interface IReservationRepository : IBaseRepository<Reservation>
{
   public List<Reservation> GetReservationsByClientId(Guid clientId);
}
