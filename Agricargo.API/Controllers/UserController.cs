using Agricargo.Application.Interfaces;
using Agricargo.Application.Models.Requests;
using Agricargo.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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

        // Método para registrar un nuevo usuario
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] UserCreateRequest request)
        {
            if (request.IsValid())
            {
                return BadRequest("Datos de usuario inválidos.");
            }

            bool result = _userService.RegisterUser(request);
            if (!result)
            {
                return BadRequest("Error al registrar el usuario. Verifica si ya existe o si tienes permisos.");
            }

            return Ok("Usuario registrado exitosamente.");
        }

        // Método para eliminar un usuario
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(User user)
        {
            var exisitingUser = _userService. // Asegúrate de tener este método en el servicio
            if (exisitingUser == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            bool result = _userService.DeleteUser(user);
            if (!result)
            {
                return BadRequest("Error al eliminar el usuario.");
            }

            return Ok("Usuario eliminado exitosamente.");
        }

        // Método para actualizar un usuario existente
        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Datos de usuario inválidos.");
            }

            var existingUser = _userService.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            // Asegúrate de que el ID en el cuerpo de la solicitud coincida con el ID en la URL
            user.Id = id;

            bool result = _userService.UpdateUser(user);
            if (!result)
            {
                return BadRequest("Error al actualizar el usuario.");
            }

            return Ok("Usuario actualizado exitosamente.");
        }
    }
}
