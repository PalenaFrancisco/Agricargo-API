using Agricargo.Application.Models.Requests;
using Agricargo.Application.Services;
using Agricargo.Domain.Entities;
using Agricargo.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agricargo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private ITripService _tripService;

        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpPost("addTrip")]
        public ActionResult Post([FromBody] TripCreateRequest tripRequest)
        {
            _tripService.Add(tripRequest);
            return Ok("Creado");
        }

        [HttpGet("getTrip/{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_tripService.Get(id));
        }

        [HttpGet("getTrips")]
        public ActionResult Get()
        {
            return Ok(_tripService.Get());
        }
    }
}
