using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Agricargo.Application.Interfaces;
using Agricargo.Application.Models.Requests;

namespace Agricargo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("UpdateUser")]
        public IActionResult UpdateUser(UpdateUserRequest userRequest) 
        {
            try
            {
                _userService.UpdateUser(userRequest, User);
                return Ok("Se actualizo el usuario");
            }
            catch (UnauthorizedAccessException ex) 
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser() 
        {
            try
            {
                _userService.DeleteUser(User);
                return Ok("Usuario eliminado");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetUserInfo")]
        public IActionResult GetUserInfo()
        {
            try
            {
                var userfind = _userService.GetUserInfo(User);
                return Ok(userfind);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }
        }

    }
}
