using System.Data;
using MySqlConnector;
using System.Reflection;
using Pokedx.Common;
using System.Data.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Common;

public class DbTransition : IDisposable
{
    private static string ConnectionString => "server=localhost;uid=poketrainer;password=password123;database=pokedex";

    private MySqlConnection DbConnection { get; set; }

    public DbTransition()
    {
        DbConnection = new MySqlConnection(ConnectionString);

        ResetConnection();
    }

    private void ResetConnection()
    {
        if (DbConnection.State == ConnectionState.Open)
        {
            DbConnection.Close();
        }

        DbConnection.Open();
    }

    public IEnumerable<T> GetFromDatabase<T>(string query, QueryOptions options)
        where T : new()
    {
        ResetConnection();

        MySqlDataReader reader = ExecuteCommand(query);

        IEnumerable<T> entities = MapInternal<T>(reader, options);

        return entities;
    }

    private MySqlDataReader ExecuteCommand(string query)
    {
        using MySqlCommand command = new MySqlCommand(query, DbConnection);

        return command.ExecuteReader();
    }

    private IEnumerable<T> MapInternal<T>(MySqlDataReader reader, QueryOptions options)
        where T : new()
    {
        List<T> entities = new List<T>();

        while (reader.Read())
        {

            T entity = CreateEntityFromDatabase<T>(reader);

            entities.Add(entity);
        }

        foreach (T entity in entities)
        {

            if (entity is IDatabaseRelatable && options.IncludeRelations)
            {
                IDatabaseRelatable relatable = (IDatabaseRelatable)entity;

                relatable.GetRelatedEntities(ConnectionString);
            }
        }

        return entities;
    }

    private T CreateEntityFromDatabase<T>(MySqlDataReader reader) where T : new()
    {
        var columns = reader.GetColumnSchema();

        T entity = new T();

        foreach (DbColumn column in columns)
        {
            Type entityType = entity.GetType();

            var properties = entityType
                .GetProperties()
                .Where(x => x.IsDefined(typeof(ColumnAttribute)));


            object columnValue = reader.GetValue(column.ColumnName);

            foreach (PropertyInfo property in properties)
            {
                ColumnAttribute? attr = (ColumnAttribute?)property.GetCustomAttribute(typeof(ColumnAttribute));

                if (attr == null || string.IsNullOrEmpty(attr.Name) || attr.Name != column.ColumnName)
                {
                    continue;
                }

                property.SetValue(entity, columnValue);
                break;
            }
        }

        return entity;
    }

    public void Insert<T>(T entity)
    {
        ResetConnection();
        string query = CommandBuilder.InsertCommand(entity);

        Console.WriteLine(query);
        MySqlCommand command = new MySqlCommand(query, DbConnection);

        command.ExecuteNonQuery();
    }

    private static IEnumerable<PropertyInfo> GetRelationProperties(Type type) =>
        type.GetProperties()
            .Where(prop => prop.IsDefined(typeof(RelationAttribute)));

    public void Insert<T>(IEnumerable<T> entities)
    {
        foreach (T entity in entities)
        {
            Insert(entity);
        }
    }

    public void Update<T>(T entity)
    {
        ResetConnection();

        string query = CommandBuilder.UpdateCommand(entity);

        MySqlCommand command = new MySqlCommand(query, DbConnection);

        command.ExecuteNonQuery();
    }

    public void Update<T>(IEnumerable<T> entities)
    {
        foreach (T entity in entities)
        {
            Update(entity);
        }
    }

    public void Delete<T>(T entity)
    {
        ResetConnection();

        string query = CommandBuilder.DeleteCommand(entity);

        MySqlCommand command = new MySqlCommand(query, DbConnection);

        command.ExecuteNonQuery();
    }

    public void Delete<T>(IEnumerable<T> entities)
    {
        foreach (T entity in entities)
        {
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