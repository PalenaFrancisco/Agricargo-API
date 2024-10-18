using Agricargo.Domain.Entities;
using Agricargo.Infrastructure.Repositories;

namespace Agricargo.Infrastructure.Data.Repositories
{
    public interface ITripRepository : IBaseRepository<Trip>
    {
        public List<Trip> GetCompanyTrips(Guid companyId);
    }
}