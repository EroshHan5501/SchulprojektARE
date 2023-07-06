
using MySqlConnector;

namespace Pokedex.Common;



public interface IDatabaseRelatable
{ 
    public void GetRelatedEntities(string connectionString);
}