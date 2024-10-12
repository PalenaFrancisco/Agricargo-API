using Agricargo.Domain.Interfaces;
using Agricargo.Application.Interfaces;
using Agricargo.Domain.Entities;
using Agricargo.Application.Models.Requests;


namespace Agricargo.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public User CreateUserByRole(UserCreateRequest request) 
        {
            switch (request.Role) 
            {
                case "Client":
                    return new Client
                    {
                        Name = request.Name,
                        Email = request.Email,
                        Password = _passwordHasher.HashPassword(request.Password)
                    };

                case "Admin":
                    return new Company
                    {
                        Name = request.Name,
                        Email = request.Email,
                        CompanyName = request.CompanyName,
                        Password = _passwordHasher.HashPassword(request.Password)
                    };

                case "SuperAdmin":
                    return new SuperAdmin
                    {
                        Name = request.Name,
                        Email = request.Email,
                        Password = _passwordHasher.HashPassword(request.Password)
                    };

                default:
                    throw new ArgumentException("Rol invalido");
            }
        }

        public bool DeleteUser(User user)
        {
            var exisitingUser = _userRepository.GetUserByEmail(user.Email);
            if (exisitingUser == null)
            {
                return false;
            }

            try
            {
                _userRepository.Delete(exisitingUser);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RegisterUser(UserCreateRequest userRequest, Guid? creatorId = null)
        {
            if (!userRequest.IsValid())
            {
                return false;
            }

            if (userRequest.Role == "Admin" || userRequest.Role == "SuperAdmin")
            {
                if (creatorId == null)
                {
                    return false;
                }

                var isCreatorSuperAdmin = _userRepository.IsSuperAdmin(creatorId.Value);
                if (!isCreatorSuperAdmin)
                {
                    return false;
                }
            }

            var existingUser = _userRepository.GetUserByEmail(userRequest.Email);
            if (existingUser != null)
            {
                return false;
            }

            try
            {
                var newUser = CreateUserByRole(userRequest);
                _userRepository.Add(newUser);
                return true;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return false;
            }
        }

        public bool UpdateUser(User user)
        {
            var existingUser = _userRepository.GetUserById(user.Id);
            if (existingUser == null)
            {
                return false;
            }

            try
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;

                if (!string.IsNullOrEmpty(user.Password))
                {
                    existingUser.Password = _passwordHasher.HashPassword(user.Password);
                }

                _userRepository.Update(existingUser);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}
