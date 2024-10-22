
using Agricargo.Application.Models.Requests;
using Agricargo.Application.Services;
using Agricargo.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agricargo.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = "AdminPolicy")]
public class ShipController : ControllerBase
{
    private IShipService _shipService;

    public ShipController(IShipService shipService)
    {
        _shipService = shipService;
    }

    [HttpPost("addShip")]
    public ActionResult Post([FromBody] ShipCreateRequest shipRequest)
    {
        _shipService.Add(shipRequest, User);
        return Ok("Creado");
    }

    [HttpPut("updateShip")]
    public IActionResult Update(int id, ShipCreateRequest shipRequest)
    {
        _shipService.Update(User, id, shipRequest);
        return Ok("Barco modificado con éxito");
    }

    [HttpDelete("deleteShip")]
    public IActionResult Delete(int id)
    {
        _shipService.Delete(User, id);
        return Ok("Barco eliminado con éxito");
    }

    [HttpGet("getShips")]
    public ActionResult Get()
    {
        return Ok(_shipService.Get(User));
    }

    [HttpGet("getShip/{id}")]
    public ActionResult GetShip(int id)
    {
        return Ok(_shipService.Get(User, id));
    }
}

