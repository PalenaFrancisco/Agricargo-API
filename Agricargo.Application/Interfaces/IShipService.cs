using Agricargo.Application.Models.Requests;
using Agricargo.Domain.Entities;


namespace Agricargo.Application.Services
{
    public interface IShipService
    {

        public Ship Get(int id);

        public List<Ship> Get();


        public void Delete(Ship ship);


        public void Add(ShipCreateRequest shipService);

        public bool IsShipOwnedByCompany(int shipId, Guid companyId);

    }
}