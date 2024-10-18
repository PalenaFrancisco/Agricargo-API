using Agricargo.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Agricargo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("addReservation")]
        [Authorize(Policy = "ClientPolicy")]
        public IActionResult MakeReservation(int tripId)
        {
            try
            {
                _reservationService.AddReservation(User, tripId);
                return Ok("Reserva creada exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("reservations")]
        [Authorize(Policy = "ClientPolicy")]
        public IActionResult GetClientReservations()
        {
            var clientReservations = _reservationService.GetReservations(User);
            return Ok(clientReservations);
        }
    }
}
