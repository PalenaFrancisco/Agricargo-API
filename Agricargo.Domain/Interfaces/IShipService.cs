using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;

namespace Agricargo.Application.Services
{
    public interface IShipService
    {

        public Ship Get(int id);

        public List<Ship> Get();


        public string Delete(int id);


        public void Add(ShipCreateRequest shipService);
  
    }
}