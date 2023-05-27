
using MySqlConnector;

namespace Pokedex.Common;

public class Image : IDatabaseMapable
{
    public int ImageId { get; set; }

    public string Url { get; set; }

    public int FPokemonId { get; set; }

    public string DatabaseName => "image";

    public string InsertCommand => $"INSERT INTO {DatabaseName}(url, fpokemonId) VALUES('{Url}', {FPokemonId})";

    public string UpdateCommand => $"UPDATE {DatabaseName} SET imageId={ImageId}, url='{Url}', fpokemonId={FPokemonId} WHERE imageId={ImageId}";

    public string DeleteCommand => $"DELETE FROM {DatabaseName} WHERE imageId={ImageId}";

    public void GetFrom(MySqlDataReader reader)
    {
        ImageId = reader.GetInt32(0);
        Url = reader.GetString(1);
    }
}