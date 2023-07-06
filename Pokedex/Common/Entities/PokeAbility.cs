using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MySqlConnector;

namespace Pokedex.Common;

[Table("pokeabilities")]
public class PokeAbility : IDatabaseMapable
{
    [Key]
    [Column("Id")]
    public int id { get; set; }

    [Column("PokeId")]
    public int pokeId { get; set; }

    [Column("AbilityId")]
    public int abilityId { get; set; }

    public void GetFrom(MySqlDataReader reader)
    {
        id = reader.GetInt32(0);
        pokeId = reader.GetInt32(1);
        abilityId = reader.GetInt32(2);
    }
}