using Agricargo.Application.Models.Requests;
using Agricargo.Domain.Entities;
using System.Security.Claims;


namespace Agricargo.Application.Services
{
    public interface IShipService
    {

        public Ship Get(ClaimsPrincipal user, int id);

        public List<Ship> Get(ClaimsPrincipal user);


        public void Delete(ClaimsPrincipal user, int id);

        public void Update(ClaimsPrincipal user, int id, ShipCreateRequest shipRequest);

    
        public void Add(ShipCreateRequest shipService, ClaimsPrincipal user);

        public bool IsShipOwnedByCompany(int shipId, Guid companyId);

    }
}