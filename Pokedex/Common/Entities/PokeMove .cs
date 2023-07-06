using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MySqlConnector;

namespace Pokedex.Common;

[Table("pokemoves")]
public class PokeMove
{
    [Key]
    [Column("Id")]
    public int id { get; set; }

    [Column("PokeId")]
    public int pokeId { get; set; }

    [Column("MoveId")]
    public int moveId { get; set; }
}