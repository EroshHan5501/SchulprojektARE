
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
        using MySqlConnection connection = new MySqlConnection(connectionString);

        connection.Open();

        using MySqlCommand command = new MySqlCommand(
            $"SELECT attackId, name, url FROM attack, pokeattack WHERE attack.attackId=pokeattack.fattackId AND pokeattack.fpokemonId={PokemonId}", 
            connection);

        MySqlDataReader reader = command.ExecuteReader();
        
        while(reader.Read()) {
            Attack attack = new Attack();

            attack.GetFrom(reader);

            Attacks.Add(attack);
        }

        connection.Close();

        using MySqlConnection connection2 = new MySqlConnection(connectionString);

        connection2.Open();

        using MySqlCommand command2 = new MySqlCommand(
            $"SELECT imageId, url FROM image WHERE fpokemonId={PokemonId}", 
            connection2);

        MySqlDataReader reader2 = command2.ExecuteReader();

        while(reader2.Read()) {
            Image image = new Image();
            image.GetFrom(reader2);

            Images.Add(image);
        }
    }
}