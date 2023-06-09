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
            Results pokemonList = JsonSerializer.Deserialize<Results>(responseBody);

            int i = 1;
            using Pokedex.Common.DbTransition transition = new Pokedex.Common.DbTransition();
            Console.WriteLine("***** ***** *****");
            if (pokemonList!= null && pokemonList.results!= null)
            {
                foreach(Pokemon pokemon in pokemonList.results) 
                {
                    Console.WriteLine(pokemon.name);
                    Console.WriteLine(pokemon.url);

                    HttpClient clientMoves = new HttpClient();
                    using HttpResponseMessage responseMoves = client.GetAsync($"{pokemon.url}").Result;
                    string responseBodyMoves = responseMoves.Content.ReadAsStringAsync().Result;
                    //Console.WriteLine(responseBodyMoves);
                    //Console.WriteLine(pokemon.url);
                    MoveList moveList = JsonSerializer.Deserialize<MoveList>(responseBodyMoves);
                    pokemon.moves = moveList.moves;
                    foreach (MoveStats mS in pokemon.moves)
                    {
                        Console.Write($"__{mS.move.name}");
                    }
                    Console.WriteLine("__");
                    //Results pokemonList = JsonSerializer.Deserialize<Results>(responseBody);

                    Pokedex.Common.Pokemon popo = new();
                    popo.PokemonId = i;
                    popo.Name = pokemon.name;
                    popo.Height = 20;
                    popo.IsDefault = false;
                    popo.BaseExperience = 0;
                    i++;
                    
                    transition.Insert(popo);
                }
            }
            Console.WriteLine("***** ***** *****");
        }

        //Testing:
        public class Pages
        {
            public string? version { get; set; }
        }

        //Actual:
        public class Pokemon
        {
            public string? name { get; set; }
            public string? url { get; set; }
            public List<MoveStats>? moves { get; set; } 
        }
        public class Results
        {
            public List<Pokemon>? results { get; set; }
        }
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
    }
}