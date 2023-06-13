
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MySqlConnector;

namespace Pokedex.Common;

[Table("attack")]
public class Attack : IDatabaseRelatable
{
    [Key]
    [Column("attackId")]
    public int AttackId { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("url")]
    public string Url { get; set; }
    
    public List<Pokemon> Pokemons { get; set; }

    public Attack()
    {
        Pokemons = new List<Pokemon>();    
    }

    public void GetFrom(MySqlDataReader reader)
    {
        AttackId = reader.GetInt32(0);
        Name = reader.GetString(1);
        Url = reader.GetString(2);
    }

    public void GetRelatedEntities(string connectionString)
    {
        using DbTransition trans = new DbTransition();

        IEnumerable<Pokemon> pokemons = trans.GetFromDatabase<Pokemon>(
            $"SELECT pokemonId, name, height, isDefault, baseExperience FROM pokemon, pokeattack WHERE pokeattack.fpokemonId=pokemon.pokemonId AND pokeattack.fattackId={AttackId}",
            new QueryOptions() { IncludeRelations = false}); 

        Pokemons = pokemons.ToList();
    }
}