
using MySqlConnector;

namespace Pokedex.Common;

// TODO: Implement mapping of database tables
public interface IDatabaseMapable 
{
    public void GetFrom(MySqlDataReader reader);

}

public interface IDatabaseRelatable : IDatabaseMapable 
{
    public void GetRelatedEntities(string connectionString);
}