using Agricargo.Application.Models.Requests;
using Agricargo.Domain.Entities;
using System.Security.Claims;
using Agricargo.Application.Models.DTOs;


namespace Agricargo.Application.Services
{
    public interface IShipService
    {

        public Ship Get(ClaimsPrincipal user, int id);

        public ShipDTO GetToDto(ClaimsPrincipal user, int id);

        public List<ShipDTO> Get(ClaimsPrincipal user);


        public void Delete(ClaimsPrincipal user, int id);

        public void Update(ClaimsPrincipal user, int id, ShipCreateRequest shipRequest);

    
        public void Add(ShipCreateRequest shipService, ClaimsPrincipal user);

        public bool IsShipOwnedByCompany(int shipId, Guid companyId);

    }
}