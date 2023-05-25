
using MySqlConnector;

namespace Pokedex.Common;

public class Image : IDatabaseMapable
{
    public int ImageId { get; set; }

    public string Url { get; set; }

    public string DatabaseName => "image";

    public string InsertCommand => throw new NotImplementedException();

    public string UpdateCommand => throw new NotImplementedException();

    public string DeleteCommand => throw new NotImplementedException();

    public void GetFrom(MySqlDataReader reader)
    {
        ImageId = reader.GetInt32(0);
        Url = reader.GetString(1);
    }
}