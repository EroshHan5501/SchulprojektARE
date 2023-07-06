
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MySqlConnector;
using Pokedx.Common;

namespace Pokedex.Common;

[Table("pokemon")]
public class Pokemon : IDatabaseRelatable
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
    [Column("Weight")]
    public int? weight { get; set; }
    [JsonPropertyName("sprites")]
    public Sprite Sprites { get; set; }
    public List<AbilityGroup> abilities { get; set; }
    public class AbilityGroup
    {
        public Ability ability { get; set; }
    }
    public class Ability
    {
        public string? name { get; set; }
        public string? url { get; set; }
    }

    //public List<StatGroup> Stats { get; set; }
    //public List<TypeGroup> Types { get; set; }
    public List<MoveGroup> moves { get; set; }
    public class MoveGroup
    {
        public Move move { get; set; }
    }
    public class Move
    {
        public string? name { get; set; }
        public string? url { get; set; }
    }

    [Relation("pokeAttack", RelationType.ManyToMany)]
    public List<Attack> Attacks { get; set; }

    [Relation("image", RelationType.OneToMany)]
    public List<Image> Images { get; set; }

    public Pokemon()
    {
        Attacks = new List<Attack>();
        Images = new List<Image>();
    }

    public void GetFrom(MySqlDataReader reader)
    {
        id = reader.GetInt32(0);
        name = reader.GetString(1);
        height = reader.GetInt32(2);
        weight = reader.GetInt32(3);
        base_experience = reader.GetInt32(4);
    }

    public void GetRelatedEntities(string connectionString)
    {
        using DbTransition trans1 = new DbTransition();

        IEnumerable<Attack> result1 = trans1.GetFromDatabase<Attack>(
            $"SELECT attackId, name, url FROM attack, pokeattack WHERE attack.attackId=pokeattack.fattackId AND pokeattack.fpokemonId={id}", 
            new QueryOptions() { IncludeRelations = false});

        Attacks = result1.ToList();

        using DbTransition trans2 = new DbTransition();

        IEnumerable<Image> result2 = trans2.GetFromDatabase<Image>($"SELECT imageId, url FROM image WHERE fpokemonId={id}", 
        new QueryOptions() { IncludeRelations = false});

        Images = result2.ToList();
    }
}