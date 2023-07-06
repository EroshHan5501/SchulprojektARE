
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MySqlConnector;
using Pokedx.Common;

namespace Pokedex.Common;

[Table("pokemon")]
public class Pokemon
{
    [Key]
    [Column("Id")]
    public int id { get; set; }
    [Column("Name")]
    public string? name { get; set; }
    //[Column("Url")]
    //public string? url { get; set; }
    [Column("BaseExperience")]
    [JsonPropertyName("base_experience")]
    public int? base_experience { get; set; }
    [Column("Height")]
    public int? height { get; set; }
    [Column("Type")]
    public string? type { get; set; }
    [Column("Weight")]
    public int? weight { get; set; }
    [Column("Hp")]
    public int? hp { get; set; }
    [Column("Attack")]
    public int? attack { get; set; }
    [Column("Defense")]
    public int? defense { get; set; }
    [Column("SpecialAttack")]
    public int? specialAttack { get; set; }
    [Column("SpecialDefense")]
    public int? specialDefense { get; set; }
    [Column("Speed")]
    public int? speed { get; set; }

    // API-Caller
    [JsonPropertyName("sprites")]
    public Sprite Sprites { get; set; }
    public List<TypeGroup> types { get; set; }

    public List<AbilityGroup> abilities { get; set; }

    public List<StatGroup> stats { get; set; }

    //public List<TypeGroup> Types { get; set; }
    public List<MoveGroup> moves { get; set; }

    // Web-API
    [JsonPropertyName("abilis")]
    public List<Ability> Abilities { get; set; }

    [JsonPropertyName("movement")]
    public List<Move> Moves { get; set; }

    public Pokemon()
    {
        Abilities = new List<Ability>();
        Moves = new List<Move>();
    }

    public void GetRelatedEntities(string connectionString)
    {
        using DbTransition trans = new DbTransition();
        QueryOptions option = new QueryOptions() {
            IncludeRelations = false
        };

        Sprite? sprite = trans
            .GetFromDatabase<Sprite>($"SELECT * FROM sprities WHERE FpokemonId={id}", option)
            .FirstOrDefault();

        if (sprite is not null) {
            Sprites = sprite;
        }

        IEnumerable<Ability> abilis = trans
            .GetFromDatabase<Ability>($"SELECT * FROM abilities, pokeabilities WHERE abilities.Id=pokeabilities.AbilityId AND pokeabilities.PokeId={id}", option);

        Abilities = abilis.ToList();

        IEnumerable<Move> mov = trans
            .GetFromDatabase<Move>($"SELECT * FROM moves, pokemoves WHERE moves.Id=pokemoves.MoveId AND pokemoves.PokeId={id}", option);

        Moves = mov.ToList();
    }
}
