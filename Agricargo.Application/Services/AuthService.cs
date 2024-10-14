using Agricargo.Application.Interfaces;
using Agricargo.Domain.Entities;
using Agricargo.Domain.Interfaces;
using System;

public enum UserRole
{
    Client,
    Admin,
    SuperAdmin
}

namespace Agricargo.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;

        public AuthService(IUserRepository userRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public User CreateUserByRole(string email, string password, string role, string name, string companyName = null)
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

        public bool Register(string email, string password, string role, string name, Guid? sysAdminId = null, string? companyName = null)
        {
            var userExist = _userRepository.GetUserByEmail(email);
            if (userExist != null)
            {
                return false;
            }

            if (role == UserRole.Admin.ToString() || role == UserRole.SuperAdmin.ToString())
            {
                if (sysAdminId == null || !_userRepository.IsSuperAdmin(sysAdminId.Value))
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
                return false;
            }
        }
    }
}
