
using MySqlConnector;

namespace Pokedex.Common;

public class Attack : IDatabaseMapable
{

    public int AttackId { get; set; }

    public string Name { get; set; }

    public string Url { get; set; }
    
    List<Pokemon> Pokemons { get; set; }

    public string DatabaseName => "attack";

    public string InsertCommand => $"INSERT INTO {DatabaseName}(name, url) VALUES ('{Name}', '{Url}')";

    public string UpdateCommand => $"UPDATE {DatabaseName} SET name='{Name}', url='{Url}' WHERE attackId={AttackId}";

    public string DeleteCommand => $"DELETE FROM {DatabaseName} WHERE attackId={AttackId}";

    public Attack()
    {
        Pokemons = new List<Pokemon>();    
    }

    public void GetFrom(MySqlDataReader reader)
    {
        AttackId = reader.GetInt32(0);
        Name = reader.GetString(1);
        Url = reader.GetString(2);
    }
}