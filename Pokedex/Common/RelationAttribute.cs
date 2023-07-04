namespace Pokedx.Common;

internal enum RelationType
{
    OneToOne=0,
    OneToMany=1,
    ManyToMany=2,
}

internal class RelationAttribute : Attribute
{
    public string RelationTableName { get; init; }

    public RelationType RelationType { get; init; }

    public RelationAttribute(string relationTableName, RelationType relationType)
    {
        RelationTableName = relationTableName;
        RelationType = relationType;    
    }
}
