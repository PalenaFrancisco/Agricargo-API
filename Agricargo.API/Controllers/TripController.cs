
using Agricargo.Application.Interfaces;
using Agricargo.Application.Models.Requests;
using Agricargo.Application.Services;
using Agricargo.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agricargo.API.Controllers;

[ApiController]
[Route("[controller]")]

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

    [HttpGet("getTrips")]
    public ActionResult Get()
    {
        return Ok(_tripService.Get());
    }

    [HttpGet("getTrip/{id}")]
    public ActionResult GetShip(int id)
    {
        return Ok(_tripService.Get(id));
    }

    [HttpPut("updateTripo/{id}")]
    public ActionResult Update(int id, [FromBody] TripCreateRequest tripRequest)
    {
        _tripService.Update(id, tripRequest);
        return Ok("Actualizado");
    }

    [HttpDelete("deleteTrip")]
    public ActionResult Delete(Trip trip)
    {
        _tripService.Delete(trip);
        return Ok("Borrado");
    }
}

