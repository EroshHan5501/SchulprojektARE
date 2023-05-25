
using System.Reflection;
using System.Text;

namespace Pokedex.Common;

public class QueryBuilder<T> where T : IDatabaseMapable {   

    private StringBuilder Query { get; set; } = null!;

    private T Entity { get; init; }

    public QueryBuilder(T entity)
    {
        InitializeInternal();
        Entity = entity;
    }   

    private IEnumerable<string> GetPropertyNames() 
    {
        Type type = typeof(T);
        
        List<string> propertyNames = new List<string>();
        foreach (PropertyInfo info in type.GetProperties()) {

            string name = info.Name;

            name = $"{char.ToLower(name[0])}{name.Substring(1)}";

            propertyNames.Add(name);
        }

        return propertyNames;
    }


    public QueryBuilder<T> SelectFrom() {
        
        IEnumerable<string> properties = GetPropertyNames();

        Query.Append("SELECT ");

        int count = 0;
        foreach (string property in properties) {
            if (count == properties.Count() - 1) {
                Query.Append($"{property}");
            }
            else {
                Query.Append($"{property}, ");
            }

            count += 1;
        }

        Query.Append($" FROM {Entity.DatabaseName}");

        return this;
    }

    private void InitializeInternal() {

        Query = new StringBuilder();
    }

    public string BuildQuery() {

        Query.Append(";");

        string query = Query.ToString();

        InitializeInternal();

        return query;
    }

}