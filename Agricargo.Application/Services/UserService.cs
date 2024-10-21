using Agricargo.Application.Interfaces;
using Agricargo.Application.Models.Requests;
using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Agricargo.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _context;
        public UserService(IUserRepository context) {
            _context = context;
        }

        private Guid GetIdFromUser(ClaimsPrincipal user)
        {
            var userId = user.FindFirst("id")?.Value;

            if (!Guid.TryParse(userId, out Guid parsedGuid))
            {
                throw new UnauthorizedAccessException("Token inválido");
            }

            return parsedGuid;
        }

        public void UpdateUser([FromBody] UpdateUserRequest userUpdate, ClaimsPrincipal user)
        {
            var userId = GetIdFromUser(user);

            var existingUser = _context.FindByGuid(userId);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("No se encontró el usuario.");
            }

            existingUser.Name = userUpdate.Name ?? existingUser.Name;

            if (!string.IsNullOrEmpty(userUpdate.Email))
            {
                existingUser.Email = userUpdate.Email;
            }

            if (!string.IsNullOrEmpty(userUpdate.Password))
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userUpdate.Password);
                existingUser.Password = hashedPassword;
            }

            if (!string.IsNullOrEmpty(userUpdate.CompanyName) && existingUser.TypeUser == "Admin")
            {
                if (existingUser is Company companyUser)
                {
                    companyUser.CompanyName = userUpdate.CompanyName;
                }
            }

            _context.Update(existingUser);
        }


        public void DeleteUser(ClaimsPrincipal user)
        {
            var userId = GetIdFromUser(user);
            var existingUser = _context.FindByGuid(userId);

            if (existingUser == null)
            {
                throw new Exception("No se encontro el usario");
            }

            if (existingUser.Id == userId || user.IsInRole("SuperAdmin"))
            {
                _context.Delete(existingUser);
            }

            throw new UnauthorizedAccessException("No esta autorizado a realizar esta accion"); 

        }

        public User GetUserInfo(ClaimsPrincipal user)
        {
            var userId = GetIdFromUser(user);
            var existingUser = _context.FindByGuid(userId);
            if(existingUser != null) 
            {
                return existingUser;
            }

            throw new Exception("No se encontro el usuario");
        }
    }
}
