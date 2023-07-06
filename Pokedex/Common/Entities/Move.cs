using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MySqlConnector;

namespace Pokedex.Common;

[Table("moves")]
public class Move 
{
    [Key]
    [Column("Id")]
    public int id { get; set; }

    [Column("Name")]
    public string name { get; set; }

    //Power???
    //Accuracy

    public void GetFrom(MySqlDataReader reader)
    {
        id = reader.GetInt32(0);
        name = reader.GetString(1);
    }
}