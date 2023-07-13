using Pokedex.Common;

namespace DbTesting;

class Program
{
    static void Main(string[] args)
    {
        using DbTransition transition = new DbTransition();

        Pokemon pokemon = transition.GetFromDatabase<Pokemon>("SELECT * FROM pokemon WHERE id=1", new QueryOptions() { IncludeRelations = true}).First();

        Console.WriteLine(pokemon.name);
        foreach (var ability in pokemon.Abilities) {
            Console.WriteLine(ability.name);
        }

        foreach (var move in pokemon.Moves) {
            Console.WriteLine(move.name);
        }

        Pokemon poke = new Pokemon() {
            id = 4303,
            name = "hello",
            base_experience = 213,
            hp = 20,
            speed = 23,
            height = 5,
        };

        transition.Insert(poke);

        Pokemon? testing = transition.GetFromDatabase<Pokemon>("SELECT * FROM pokemon WHERE id=4303", new QueryOptions()).FirstOrDefault();

        Console.WriteLine(testing.name);

        poke.name = "testing";

        transition.Update(poke);

        Pokemon? testing2 = transition.GetFromDatabase<Pokemon>("SELECT * FROM pokemon WHERE id=4303", new QueryOptions()).FirstOrDefault();

        Console.WriteLine(testing2.name);

        transition.Delete(poke);
    }
}
