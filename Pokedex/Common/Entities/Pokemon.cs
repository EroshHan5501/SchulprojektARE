
using System.Diagnostics;
using MySqlConnector;

namespace Pokedex.Common;

public class Pokemon : IDatabaseRelatable
{   
    public int PokemonId { get; set; }

    public string Name { get; set; }

    public int Height { get; set; }

    public bool IsDefault { get; set; }

    public int BaseExperience { get; set; }

    public List<Attack> Attacks { get; set; }

    public List<Image> Images { get; set; }

    public string DatabaseName => "pokemon";

    public string InsertCommand => 
        $"INSERT INTO {DatabaseName}(pokemonId, name, height, isDefault, baseExperience) VALUES({PokemonId}, '{Name}', {Height}, {IsDefault}, {BaseExperience});";

    public string UpdateCommand => 
        $"UPDATE {DatabaseName} SET pokemonId={PokemonId}, name='{Name}', height={Height}, isDefault={IsDefault}, baseExperience={BaseExperience} WHERE pokemonId={PokemonId};";

    public string DeleteCommand => $"DELETE FROM {DatabaseName} WHERE pokemonId={PokemonId};";

    public Pokemon()
    {
        Attacks = new List<Attack>();
        Images = new List<Image>();
    }

    public void GetFrom(MySqlDataReader reader)
    {
        PokemonId = reader.GetInt32(0);
        Name = reader.GetString(1);
        Height = reader.GetInt32(2);
        IsDefault = reader.GetBoolean(3);
        BaseExperience = reader.GetInt32(4);
    }

    public void GetRelatedEntities(string connectionString)
    {
        using DbTransition trans1 = new DbTransition();

        IEnumerable<Attack> result1 = trans1.GetFromDatabase<Attack>(
            $"SELECT attackId, name, url FROM attack, pokeattack WHERE attack.attackId=pokeattack.fattackId AND pokeattack.fpokemonId={PokemonId}", 
            new QueryOptions() { IncludeRelations = false});

        Attacks = result1.ToList();


        using DbTransition trans2 = new DbTransition();

        IEnumerable<Image> result2 = trans2.GetFromDatabase<Image>($"SELECT imageId, url FROM image WHERE fpokemonId={PokemonId}", 
        new QueryOptions() { IncludeRelations = false});

        Images = result2.ToList();
    }
}