
using Agricargo.Application.Services;
using Agricargo.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agricargo.API.Controllers;

[ApiController]
[Route("[controller]")]

public class ShipController : ControllerBase
{
    private IShipService _shipService;

    public ShipController(IShipService shipService)
    {
        _shipService = shipService;
    }

    [HttpPost("addShip")]
    public ActionResult Post(string type, float capacity, string captain)
    {
        _shipService.Add(type, capacity, captain);
        return Ok("Creado");
    }

    [HttpGet("getShips")]
    public ActionResult Get()
    {
        return Ok(_shipService.Get());
    }

    [HttpGet("getShip")]
    public ActionResult GetShip(int id)
    {
        return Ok(_shipService.Get(id));
    }
}

