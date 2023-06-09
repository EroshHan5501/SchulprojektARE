
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Diagnostics;
//using System.Text.Json.Serialization;
//using MySqlConnector;

//namespace Pokedex.Common;

//[Table("pokemon")]
//public class Pokemon : IDatabaseRelatable
//{
//    [Key]
//    [Column("pokemonId")]
//    public int PokemonId { get; set; }

//    [Column("name")]
//    public string Name { get; set; }

//    [Column("height")]
//    public int Height { get; set; }

//    [Column("isDefault")]
//    public bool IsDefault { get; set; }

//    [Column("baseExperience")]
//    public int BaseExperience { get; set; }

//    public List<Attack> Attacks { get; set; }

//    public List<Image> Images { get; set; }

//    public Pokemon()
//    {
//        Attacks = new List<Attack>();
//        Images = new List<Image>();
//    }

//    public void GetFrom(MySqlDataReader reader)
//    {
//        PokemonId = reader.GetInt32(0);
//        Name = reader.GetString(1);
//        Height = reader.GetInt32(2);
//        IsDefault = reader.GetBoolean(3);
//        BaseExperience = reader.GetInt32(4);
//    }

//    public void GetRelatedEntities(string connectionString)
//    {
//        using DbTransition trans1 = new DbTransition();

//        IEnumerable<Attack> result1 = trans1.GetFromDatabase<Attack>(
//            $"SELECT attackId, name, url FROM attack, pokeattack WHERE attack.attackId=pokeattack.fattackId AND pokeattack.fpokemonId={PokemonId}", 
//            new QueryOptions() { IncludeRelations = false});

//        Attacks = result1.ToList();

//        using DbTransition trans2 = new DbTransition();

//        IEnumerable<Image> result2 = trans2.GetFromDatabase<Image>($"SELECT imageId, url FROM image WHERE fpokemonId={PokemonId}", 
//        new QueryOptions() { IncludeRelations = false});

//        Images = result2.ToList();
//    }
//}