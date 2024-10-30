using Agricargo.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Agricargo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ClientPolicy")]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet("getFavorites")]
        public IActionResult Get()
        {
            try
            {
                return Ok(_favoriteService.GetFavorites(User));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("addFavorite")]
        public IActionResult Post(int id)
        {
            try
            {
                _favoriteService.AddFavorite(User, id);
                return Ok("Favorito agregado a la lista");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteFavorite")]
        public IActionResult Delete(int id)
        {
            try
            {
                _favoriteService.DeleteFavorite(User, id);
                return Ok("Favorito eliminado de la lista");
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
