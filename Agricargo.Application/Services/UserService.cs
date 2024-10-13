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
        private readonly TokenService _tokenService;


        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, TokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public User CreateUserByRole(UserCreateRequest request) 
        {
            switch (request.Role) 
            {
                case "Client":
                    return new Client
                    {
                        Id = Guid.NewGuid(),
                        Name = request.Name,
                        Email = request.Email,
                        Password = _passwordHasher.HashPassword(request.Password)
                    };

                case "Admin":
                    return new Company
                    {
                        Id = Guid.NewGuid(),
                        Name = request.Name,
                        Email = request.Email,
                        CompanyName = request.CompanyName,
                        Password = _passwordHasher.HashPassword(request.Password)
                    };

                case "SuperAdmin":
                    return new SuperAdmin
                    {
                        Id = Guid.NewGuid(),
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

        public string RegisterUser(UserCreateRequest userRequest, Guid? creatorId = null)
        {

            if (userRequest.Role == "Admin" || userRequest.Role == "SuperAdmin")
            {
                if (creatorId == null)
                {
                    return "SysAdmin Key not found";
                }

                var isCreatorSuperAdmin = _userRepository.IsSuperAdmin(creatorId.Value);
                if (!isCreatorSuperAdmin)
                {
                    return "You are not SysAdmin";
                }
            }

            var existingUser = _userRepository.GetUserByEmail(userRequest.Email);
            if (existingUser != null)
            {
                return "There is already a user with that email";
            }

            try
            {
                var newUser = CreateUserByRole(userRequest);
                _userRepository.Add(newUser);
                return _tokenService.GenerateToken(newUser);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return "Something not worked, try again";
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

        public string Login(string email, string password)
        { 
            var user = _userRepository.GetUserByEmail(email);
            if (user == null || _passwordHasher.VerifyPassword(password, user.Password) == false) 
            {
                return "Email or Password incorrect";
            }

            var token = _tokenService.GenerateToken(user);
            return token;
        }   
    }

}
