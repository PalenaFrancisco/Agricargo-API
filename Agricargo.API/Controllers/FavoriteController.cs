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
            return Ok(_favoriteService.GetFavorites(User));
        }

        [HttpPost("addFavorite")]
        public IActionResult Post(int id)
        {
            _favoriteService.AddFavorite(User, id);
            return Ok("Favorito agregado a la lista");
        }
        [HttpDelete("deleteFavorite")]
        public IActionResult Delete(int id)
        {
            _favoriteService.DeleteFavorite(User, id);
            return Ok("Favorito eliminado de la lista");
        }
    }
}
