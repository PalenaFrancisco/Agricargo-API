using Agricargo.Domain.Entities;

namespace Agricargo.Domain.Interfaces
{
    public interface IShipRepository
    {
        public void Add(Ship ship);
        public bool Delete(int id);
        public Ship Get(int id);
        public void Update(int id);
     
    }
}