
using MySqlConnector;

namespace Pokedex.Common;

// TODO: Implement mapping of database tables
public interface IDatabaseMapable {

    public string DatabaseName { get; }
    
    public string InsertCommand { get; }

    public string UpdateCommand { get; }
    
    public string DeleteCommand { get; }

    public void GetFrom(MySqlDataReader reader);

}