//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Text.Json.Serialization;
//using MySqlConnector;

//namespace Pokedex.Common;

//[Table("moves")]
//public class Move : IDatabaseMapable
//{
//    [Key]
//    [Column("Id")]
//    public int id { get; set; }

//    [Column("Name")]
//    [JsonPropertyName("front_default")]
//    public string Name { get; set; }

//    [Column("back")]
//    [JsonPropertyName("back_default")]
//    public string Back { get; set; }

//    [Column("fpokemonId")]
//    public int FPokemonId { get; set; }

//    public void GetFrom(MySqlDataReader reader)
//    {
//        id = reader.GetInt32(0);
//        Front = reader.GetString(1);
//        Back = reader.GetString(2);
//    }
//}