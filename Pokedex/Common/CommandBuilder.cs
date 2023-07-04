using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;

namespace Pokedex.Common;

public class CommandBuilder
{
    private enum CommandOption
    {
        Insert = 0,
        Update = 1,
        Delete = 2
    };

    public static string InsertCommand<T>(T entity) 
    {
        string tableName = GetTableName(typeof(T));

        StringBuilder query = new StringBuilder();

        query.Append($"INSERT INTO {tableName}(");

        List<KeyValuePair<string, string>> properties = GetColumnProperties(typeof(T), entity).ToList();

        bool first = true;
        foreach (KeyValuePair<string, string> property in properties)
        {
            if (first)
            {
                query.Append($"{property.Key}");
                first = false;
            }
            else
            {
                query.Append($", {property.Key}");
            }
        }


        query.Append(") VALUES(");

        first = true;
        foreach (KeyValuePair<string, string> property in properties)
        {
            if (first)
            {
                query.Append($"{property.Value}");
                first = false;
            }
            else
            {
                query.Append($", {property.Value}");
            }
        }

        query.Append(");");

        return query.ToString();
    }

    public static string UpdateCommand<T>(T entity)
    {
        string tableName = GetTableName(typeof(T));

        StringBuilder query = new StringBuilder();

        query.Append($"UPDATE {tableName} SET");

        bool first = true;
        // iterate over every column property
        foreach (KeyValuePair<string, string> property in GetColumnProperties(typeof(T), entity))
        {
            if (first)
            {
                query.Append($" {property.Key}={property.Value}");
                first = false;
            }
            else 
            {
                query.Append($", {property.Key}={property.Value}");

            }
        }

        (string name, int value) = GetKeyProperty<T>(typeof(T), entity);

        query.Append($" WHERE {name}={value};");

        return query.ToString();
    }

    public static string DeleteCommand<T>(T entity)
    {
        string tableName = GetTableName(typeof(T));

        StringBuilder query = new StringBuilder();

        (string name, int value) = GetKeyProperty<T>(typeof(T), entity);

        query.Append($"DELETE FROM {tableName} WHERE {name}={value};");

        return query.ToString();
    }

    private static string GetTableName(Type type)
    { 
        TableAttribute? tableAttr = (TableAttribute?)type.GetCustomAttribute(typeof(TableAttribute));

        if (tableAttr is null)
        {
            throw new Exception("Entity is not suitable for database mapping");
        }

        return tableAttr.Name;
    }

    private static KeyValuePair<string, int> GetKeyProperty<T>(Type type, T entity)
    {
        IEnumerable<PropertyInfo> keyProperties = type
            .GetProperties()
            .Where(prop => prop.IsDefined(typeof(KeyAttribute), false));
    
        if (keyProperties.Count() != 1)
        {
            throw new Exception("Key attribute has to be applied only once!");
        }
        
        PropertyInfo keyProperty = keyProperties.First();

        string name = keyProperty.Name;
        int? value = (int?)keyProperty.GetValue(entity);

        if (value == null)
        {
            throw new Exception("Value of key can't be null");
        }
        
        return new KeyValuePair<string, int>(name, (int)value);
    }

    private static IEnumerable<KeyValuePair<string, string>> GetColumnProperties<T>(Type type, T entity)
    {
        IEnumerable<PropertyInfo> columnProperties = type
            .GetProperties()
            .Where(prop => prop.IsDefined(typeof(ColumnAttribute), false)); 

        foreach (PropertyInfo property in columnProperties)
        {
            ColumnAttribute? attr = (ColumnAttribute?)property.GetCustomAttribute(typeof(ColumnAttribute)); 
            if (attr is null)
            {
                throw new Exception("Column attribute is not defined");
            }
            string name = attr.Name;
            string? value;
            if (property.GetValue(entity) == null)
            {
                value = "null";
            }
            else
            {
                value = property.GetValue(entity).ToString();
            }

            if (property.PropertyType == typeof(string))
            {
                value = $"'{value}'";
            }

            yield return new KeyValuePair<string, string> (name, value);
        }
    }
}
