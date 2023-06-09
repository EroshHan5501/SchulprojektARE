
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Common;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public sealed class PokemonController : ControllerBase {

    public PokemonController() {

    } 

    [HttpGet("overview/")]
    public IActionResult GetOverview() {
        
        using DbTransition transition = new DbTransition();

        IEnumerable<Pokemon> pokemons = transition.GetFromDatabase<Pokemon>("SELECT * FROM pokemon", new QueryOptions() { IncludeRelations = true});

        return Ok(pokemons);
    }

    [HttpGet]
    public IActionResult Get([FromQuery]int pokemonId) {

        return Ok("Hello world");
    }
}