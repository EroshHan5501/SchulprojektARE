using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MySqlConnector;

namespace Pokedex.Common;

[Table("image")]
public class Image : IDatabaseMapable
{
    [Key]
    [Column("imageId")]
    public int ImageId { get; set; }

    [Column("url")]
    public string Url { get; set; }

    [Column("fpokemonId")]
    public int FPokemonId { get; set; }

    public void GetFrom(MySqlDataReader reader)
    {
        ImageId = reader.GetInt32(0);
        Url = reader.GetString(1);
    }
}