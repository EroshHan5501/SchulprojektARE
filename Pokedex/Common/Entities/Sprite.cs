using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MySqlConnector;

namespace Pokedex.Common;

[Table("sprites")]
public class Sprite
{
    [Key]
    [Column("Id")]
    public int id { get; set; }

    [Column("Front")]
    [JsonPropertyName("front_default")]
    public string? Front { get; set; }

    [Column("Back")]
    [JsonPropertyName("back_default")]
    public string? Back { get; set; }

    [Column("FpokemonId")]
    public int FPokemonId { get; set; }
}