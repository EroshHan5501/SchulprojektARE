using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MySqlConnector;

namespace Pokedex.Common;

[Table("abilities")]
public class Ability
{
    [Key]
    [Column("Id")]
    public int id { get; set; }

    [Column("Name")]
    public string name { get; set; }

    [Column("Entry")]
    public string entry { get; set; }
    
    [Column("ShortEntry")]
    public string shortEntry { get; set; }

    public List<EffectGroup> effect_entries { get; set; }
    public class EffectGroup
    {
        public string effect { get; set; }
        public Language language { get; set; }
        public string short_effect { get; set; }
    }

    public class Language
    {
        public string name { get; set; }
    }
}