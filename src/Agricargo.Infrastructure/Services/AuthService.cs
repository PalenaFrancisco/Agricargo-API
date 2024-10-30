using Agricargo.Application.Interfaces;
using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;
using System;
using System.Security.Claims;

namespace Agricargo.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;

        public enum UserRole
        {
            Client,
            Admin,
            SuperAdmin
        }

        public AuthService(IUserRepository userRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        private Guid GetCompanyIdFromUser(ClaimsPrincipal user)
        {
            var userId = user.FindFirst("id")?.Value;

            if (!Guid.TryParse(userId, out Guid parsedGuid))
            {
                throw new UnauthorizedAccessException("Token inválido");
            }

            return parsedGuid;
        }

        public User CreateUserByRole(string email, string password, string role, string name, string? companyName)
        {
            if (!Enum.TryParse(role, out UserRole userRole))
            {
                throw new ArgumentException("Invalid role");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            return userRole switch
            {
                UserRole.Client => new Client
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    Password = hashedPassword
                },
                UserRole.Admin => new Company
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    CompanyName = companyName ?? throw new ArgumentException("Company name is required for Admin"),
                    Password = hashedPassword
                },
                UserRole.SuperAdmin => new SuperAdmin
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    Password = hashedPassword
                },
                _ => throw new ArgumentException("Invalid role"),
            };
        }

        public string Login(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null; 
            }

            return _tokenService.GenerateToken(user);
        }

        public bool Register(ClaimsPrincipal user, string email, string password, string role, string name, string? companyName)
        {
            var userExist = _userRepository.GetUserByEmail(email);
            if (userExist != null)
            {
                return false;
            }
            
            if (role == UserRole.Admin.ToString() || role == UserRole.SuperAdmin.ToString())
            {
                var userId = GetCompanyIdFromUser(user);
                
                if (userId == null || !_userRepository.IsSuperAdmin(userId))
                {
                    return false;
                }
            }

            try
            {
                var newUser = CreateUserByRole(email, password, role, name, companyName);
                _userRepository.Add(newUser);

                return !string.IsNullOrEmpty(_tokenService.GenerateToken(newUser));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
