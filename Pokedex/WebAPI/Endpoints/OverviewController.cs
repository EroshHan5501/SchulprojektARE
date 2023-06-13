
using Microsoft.AspNetCore.Mvc;

namespace Pokedex.WebApi;

[Route("api/[controller]")]
[ApiController]
public class OverviewController : ControllerBase
{
    [HttpGet]
    public IActionResult Get([FromQuery] string names)
    {

        return Ok("{\"test\": \"testing\"}");
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {

        return Ok(id);
    }
}