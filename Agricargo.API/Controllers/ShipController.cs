
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
        try
        {
            _shipService.Add(shipRequest, User);
            return Ok("Creado");
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("updateShip/{id}")]
    public IActionResult Update(int id, [FromBody] ShipCreateRequest shipRequest)
    {
        try
        {
            _shipService.Update(User, id, shipRequest);
            return Ok("Barco modificado con éxito.");
        }
        catch(UnauthorizedAccessException ex) {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    [HttpDelete("deleteShip/{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _shipService.Delete(User, id);
            return Ok("Barco eliminado con éxito.");
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

    [HttpGet("getShips")]
    public ActionResult Get()
    {
        try
        {
            return Ok(_shipService.Get(User));
        }
        catch (Exception ex) 
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("getShip/{id}")]
    public ActionResult GetShip(int id)
    {
        try
        {
            return Ok(_shipService.GetToDto(User, id));
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

