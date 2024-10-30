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
        public IActionResult MakeReservation(int tripId, float amountReserved)
        {
            try
            {
                _reservationService.AddReservation(User, tripId, amountReserved);
                return Ok("Reserva creada exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("clientReservations")]
        [Authorize(Policy = "ClientPolicy")]
        public IActionResult GetClientReservations()
        {
            try
            {

                var clientReservations = _reservationService.GetClientReservations(User);
                return Ok(clientReservations);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("companyReservations")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult GetCompanyReservations()
        {
            try
            {
                var clientReservations = _reservationService.GetCompanyReservations(User);
                return Ok(clientReservations);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteReservations/{id}")]
        [Authorize(Policy = "AllPolicy")]
        public IActionResult DeleteReservation(int id)
        {
            try
            {
                _reservationService.DeleteReservation(id, User);
                return Ok("Reserva eliminada");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
