using Agricargo.Domain.Entities;
using Agricargo.Infrastructure.Repositories;

namespace Agricargo.Domain.Interfaces
{
    public interface IShipRepository : IBaseRepository <Ship>
    {

        public List<Ship> GetCompanyShips(Guid companyId);
    }
}