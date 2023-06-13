using System.Data;
using Pokedex.Common;
using MySqlConnector;
using System.Reflection;
using Pokedx.Common;

namespace Pokedex.Common;

public class DbTransition : IDisposable {
    private static string ConnectionString => "server=localhost;uid=poketrainer;password=password123;database=pokedex";

    private MySqlConnection DbConnection { get; set; }

    public DbTransition()
    {
        DbConnection = new MySqlConnection(ConnectionString);

        if (DbConnection.State == ConnectionState.Open) {
            DbConnection.Close();
        }

        DbConnection.Open();
    }
    
    public IEnumerable<T> GetFromDatabase<T>(string query, QueryOptions options) 
        where T : IDatabaseMapable, new()
    {
        MySqlDataReader reader = ExecuteCommand(query);

        IEnumerable<T> entities = MapInternal<T>(reader, options);

        return entities;
    }

    private MySqlDataReader ExecuteCommand(string query) {
        using MySqlCommand command = new MySqlCommand(query, DbConnection);

        return command.ExecuteReader();
    }

    private IEnumerable<T> MapInternal<T>(MySqlDataReader reader, QueryOptions options) 
        where T : IDatabaseMapable, new()
    {
        List<T> entities = new List<T>();

        while(reader.Read()) {

            T entity = new T();

            entity.GetFrom(reader); 

            entities.Add(entity);
        }

        foreach (T entity in entities) {
            
            if (entity is IDatabaseRelatable && options.IncludeRelations) {
                IDatabaseRelatable relatable = (IDatabaseRelatable)entity;

                relatable.GetRelatedEntities(ConnectionString);
            }
        }

        return entities;
    }

    public void Insert<T>(T entity) 
        where T : IDatabaseMapable 
    {
        string query = CommandBuilder.InsertCommand(entity);

        MySqlCommand command = new MySqlCommand(query, DbConnection);

        command.ExecuteNonQuery();

        IEnumerable<PropertyInfo> relationProperties = GetRelationProperties(typeof(T));

        List<Type> types = new List<Type>();
        foreach (PropertyInfo property in relationProperties)
        {
            

           property.GetMethod.Invoke(entity);
        }


        // Here we need to create the relation ships
       
        // First we get properties with the relation attribute 

        // Then we check which relation type is used 

        // If it is a many to many relationship we 
    }

    private static IEnumerable<PropertyInfo> GetRelationProperties(Type type) =>
        type.GetProperties()
            .Where(prop => prop.IsDefined(typeof(RelationAttribute)));

    public void Insert<T>(IEnumerable<T> entities) 
        where T : IDatabaseMapable 
    {
        foreach(T entity in entities) {
            Insert(entity);
        }
    }

    public void Update<T>(T entity) 
        where T : IDatabaseMapable
    {
        string query = CommandBuilder.UpdateCommand(entity);

        MySqlCommand command = new MySqlCommand(query, DbConnection);

        command.ExecuteNonQuery();
    }

    public void Update<T>(IEnumerable<T> entities) 
        where T : IDatabaseMapable
    {
        foreach (T entity in entities) {
            Update(entity);
        }
    }

    public void Delete<T>(T entity) 
        where T : IDatabaseMapable
    {
        string query = CommandBuilder.DeleteCommand(entity);

        MySqlCommand command = new MySqlCommand(query, DbConnection);

        command.ExecuteNonQuery();
    }

    public void Delete<T>(IEnumerable<T> entities) 
        where T : IDatabaseMapable
    {
        foreach (T entity in entities) {
            Delete(entity);
        }
    }

    private bool isDisposed = false;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool dispose) 
    {
        if (!dispose || !isDisposed) 
        {
            return;
        }

        DbConnection.Close();
    }
}