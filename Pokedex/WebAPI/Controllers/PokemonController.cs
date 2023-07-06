
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

    [HttpGet]
    public IActionResult GetOverview([FromQuery] int pageNumber, [FromQuery] int pageSize) {
        
        using DbTransition transition = new DbTransition();


        IEnumerable<Pokemon> pokemons = transition
            .GetFromDatabase<Pokemon>(
                $"SELECT * FROM pokemon LIMIT {(pageNumber - 1) * pageSize}, {pageSize}", 
                new QueryOptions() { IncludeRelations = true});

        return Ok(pokemons);
    }

    [HttpGet("detail/")]
    public IActionResult Get([FromQuery]int pokemonId) {

        using DbTransition transition = new DbTransition();

        string query = $"SELECT * FROM pokemon WHERE pokemonId={pokemonId}";

        Pokemon? pokemon = transition
            .GetFromDatabase<Pokemon>(
                query, 
                new QueryOptions() { IncludeRelations = true})
            .SingleOrDefault();

        if (pokemon is null) 
            return NotFound($"No pokemon with the id {pokemonId}");

        return Ok(pokemon);
    }
}