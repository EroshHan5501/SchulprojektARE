using MySqlConnector;
using Pokedex.Common;
using System.Text.Json;

namespace ApiCaller
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //CLI input
            string input = String.Empty;
            string apiString = "https://pokeapi.co/api/v2/";
            if (args.Length > 0)
            {
                input = args[0];
            }

            //Api request
            HttpClient client = new HttpClient();
            using HttpResponseMessage response = client.GetAsync($"{apiString}{input}").Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(responseBody);


            //Deserialize json-string
            //Testing:
            Pages seite = JsonSerializer.Deserialize<Pages>(responseBody);
            Console.WriteLine(seite.version);

            //Actual:
            PokeList pokemonList = JsonSerializer.Deserialize<PokeList>(responseBody);

            int i = 1;
            using Pokedex.Common.DbTransition transition = new Pokedex.Common.DbTransition();
            Console.WriteLine("***** ***** *****");
            if (pokemonList!= null && pokemonList.results!= null)
            {
                foreach(ProtoPokemon pokemon in pokemonList.results) 
                {
                    Console.WriteLine(pokemon.name);
                    Console.WriteLine(pokemon.url);

                    HttpClient newClient = new HttpClient();
                    using HttpResponseMessage newResponse = newClient.GetAsync(pokemon.url).Result;
                    string newResponseBody = newResponse.Content.ReadAsStringAsync().Result;
                    Pokemon newPokemon = JsonSerializer.Deserialize<Pokemon>(newResponseBody);
                    Console.WriteLine();
                    Console.WriteLine("***** ***** *****");
                    Console.WriteLine(newPokemon.name);
                    Console.WriteLine(newPokemon.url);
                    Console.WriteLine(newPokemon.height);
                    Console.WriteLine(newPokemon.base_experience);
                    Console.WriteLine(newPokemon.types[0].type.name);
                    Console.WriteLine(newPokemon.sprites.front_default);
                    Console.WriteLine(newPokemon.moves[0].move.name);
                    Console.WriteLine("***** ***** *****");
                    Console.WriteLine();

                    /*
                    HttpClient clientMoves = new HttpClient();
                    using HttpResponseMessage responseMoves = client.GetAsync($"{pokemon.url}").Result;
                    string responseBodyMoves = responseMoves.Content.ReadAsStringAsync().Result;
                    //Console.WriteLine(responseBodyMoves);
                    //Console.WriteLine(pokemon.url);
                    MoveList moveList = JsonSerializer.Deserialize<MoveList>(responseBodyMoves);
                    Pokemon detailedPokemon = JsonSerializer.Deserialize<Pokemon>(responseBodyMoves);
                    pokemon.moves = moveList.moves;
                    foreach (MoveStats mS in pokemon.moves)
                    {
                        Console.Write($"__{mS.move.name}");
                    }
                    Console.WriteLine("__");
                    //Results pokemonList = JsonSerializer.Deserialize<Results>(responseBody);

                    Console.WriteLine("aaaaaaaaaaaaaaaaaaa");
                    Console.WriteLine(detailedPokemon.name);
                    Console.WriteLine(detailedPokemon.url);
                    Console.WriteLine(detailedPokemon.id);
                    Console.WriteLine(detailedPokemon.height);
                    Console.WriteLine(detailedPokemon.base_experience);
                    Console.WriteLine(detailedPokemon.sprites.front_default);
                    Console.WriteLine("aaaaaaaaaaaaaaaaaaa");

                    Pokedex.Common.Pokemon popo = new();
                    popo.PokemonId = detailedPokemon.id;
                    popo.Name = pokemon.name;
                    popo.Height = detailedPokemon.height;
                    popo.IsDefault = detailedPokemon.IsDefault;
                    popo.BaseExperience = detailedPokemon.base_experience;
                    i++;
                    
                    //transition.Insert(popo);
                    */
                }
            }
            Console.WriteLine("***** ***** *****");
        }

        //Testing:
        public class Pages
        {
            public string? version { get; set; }
        }
        /*
        //Actual:
        public class Pokemon
        {
            public int id { get; set; }
            public int height { get; set; }
            public string? name { get; set; }
            public string? url { get; set; }
            public bool IsDefault { get; set; }
            public int base_experience { get; set; }
            public List<MoveStats>? moves { get; set; }
            public Sprites sprites { get; set; }
        }
        public class Results
        {
            public List<Pokemon>? results { get; set; }
        }
        //Moves
        public class Move
        {
            public string? name { get; set; }
            public string? url { get; set; }
        }
        public class MoveStats
        {
            public Move? move { get; set; }
        }

        public class MoveList
        {
            public List<MoveStats>? moves { get; set; }
        }

        //Images
        public class Sprites
        {
            public string front_default { get; set; }
        }
        //public class Sprites
        //{
        //    public List<Image> sprites { get; set; }
        //}
        */

        //Better Attempt:
        public class PokeList
        {
            public List<ProtoPokemon> results { get; set; }
        }

        public class ProtoPokemon
        {
            public string? name { get; set; }
            public string? url { get; set; }
        }

        public class Pokemon
        {
            public int? id { get; set; }
            public string? name { get; set; }
            public string? url { get; set; }
            public int? base_experience { get; set; }
            public int? height { get; set; }
            public int? weight { get; set; }
            public List<AbilityGroup> abilities { get; set; }
            public List<StatGroup> stats { get; set; }
            public List<TypeGroup> types { get; set; }
            public List<MoveGroup> moves { get; set; }
            public Sprites sprites { get; set; }

            public class AbilityGroup
            {
                Ability ability { get; set; }
            }
            public class Ability
            {
                public string? name { get; set; }
                public string? url { get; set; }
            }

            public class StatGroup
            {
                public int? base_stat { get; set; }
                public int? effort { get; set; }
                public Stat stat { get; set; }
            }
            public class Stat
            {
                public string? name { get; set; }
                public string? url { get; set; }
            }

            public class TypeGroup
            {
                public int? slot { get; set; }
                public Type type { get; set; }
            }
            public class Type
            {
                public string? name { get; set; }
                public string? url { get; set; }
            }

            public class MoveGroup
            {
                public Move move { get; set; }
            }
            public class Move
            {
                public string? name { get; set; }
                public string? url { get; set; }
            }

            public class Sprites
            {
                public string? front_default { get; set; }
                public string? back_default { get; set; }
            }
        }
    }
}