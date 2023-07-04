using MySqlConnector;
using Pokedex.Common;
using System.Text.Json;
using System.Text.Json.Serialization;

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



            //Actual:
            ProtoList pokemonList = JsonSerializer.Deserialize<ProtoList>(responseBody);

            using Pokedex.Common.DbTransition transition = new Pokedex.Common.DbTransition();
            Console.WriteLine("***** ***** *****");
            if (pokemonList!= null && pokemonList.results!= null)
            {
                foreach(ProtoObject pokemon in pokemonList.results) 
                {
                    //Console.WriteLine(pokemon.name);
                    //Console.WriteLine(pokemon.url);

                    HttpClient newClient = new HttpClient();
                    using HttpResponseMessage newResponse = newClient.GetAsync(pokemon.url).Result;
                    string newResponseBody = newResponse.Content.ReadAsStringAsync().Result;
                    Pokedex.Common.Pokemon newPokemon = JsonSerializer.Deserialize<Pokedex.Common.Pokemon>(newResponseBody);
                    Sprite newSprites = new();
                    Console.WriteLine(newPokemon.Sprites.Front);
                    newSprites.Front = newPokemon.Sprites.Front; 
                    newSprites.Back = newPokemon.Sprites.Back;
                    newSprites.FPokemonId = newPokemon.id;
                    Console.WriteLine();
                    Console.WriteLine(newSprites.Front);
                    Console.WriteLine(newSprites.Back);
                    Console.WriteLine("***** ***** *****");
                    Console.WriteLine(newPokemon.name);
                    //Console.WriteLine(newPokemon.url);
                    Console.WriteLine(newPokemon.height);
                    Console.WriteLine(newPokemon.base_experience);
                    //Console.WriteLine(newPokemon.types[0].type.name);
                    //Console.WriteLine(newPokemon.sprites.front_default);
                    //Console.WriteLine(newPokemon.moves[0].move.name);
                    Console.WriteLine("***** ***** *****");
                    Console.WriteLine();

                    
                    //transition.Insert(newPokemon);
                    //transition.Insert(newSprites);
                }
            }
            Console.WriteLine("***** ***** *****");

            //Api request
            HttpClient clientProtoMove = new HttpClient();
            using HttpResponseMessage responseProtoMove = clientProtoMove.GetAsync($"{apiString}move").Result;
            string responseBodyProtoMove = responseProtoMove.Content.ReadAsStringAsync().Result;
            Console.WriteLine(responseBodyProtoMove);
            ProtoList protoMoveList = JsonSerializer.Deserialize<ProtoList>(responseBodyProtoMove);
            if (protoMoveList != null && protoMoveList.results != null)
            {
                foreach (ProtoObject protoMove in protoMoveList.results)
                {
                    HttpClient newClient = new HttpClient();
                    using HttpResponseMessage newResponse = newClient.GetAsync(protoMove.url).Result;
                    string newResponseBody = newResponse.Content.ReadAsStringAsync().Result;
                    Pokedex.Common.Move newMove = JsonSerializer.Deserialize<Pokedex.Common.Move>(newResponseBody);
                    Console.WriteLine(newMove.id);
                    Console.WriteLine(newMove.name);
                }
            }
        }

        //Better Attempt:
        public class ProtoList
        {
            public List<ProtoObject> results { get; set; }
        }

        public class ProtoObject
        {
            public string? name { get; set; }
            public string? url { get; set; }
        }

        //public class Pokemon
        //{
        //    public int? id { get; set; }
        //    public string? name { get; set; }
        //    public string? url { get; set; }
        //    [JsonPropertyName("Base_experience")]
        //    public int? BaseExperience { get; set; }
        //    public int? height { get; set; }
        //    public int? weight { get; set; }
        //    public List<AbilityGroup> abilities { get; set; }
        //    public List<StatGroup> stats { get; set; }
        //    public List<TypeGroup> types { get; set; }
        //    public List<MoveGroup> moves { get; set; }
        //    public Sprites sprites { get; set; }

        //    public class AbilityGroup
        //    {
        //        Ability ability { get; set; }
        //    }
        //    public class Ability
        //    {
        //        public string? name { get; set; }
        //        public string? url { get; set; }
        //    }

        //    public class StatGroup
        //    {
        //        public int? base_stat { get; set; }
        //        public int? effort { get; set; }
        //        public Stat stat { get; set; }
        //    }
        //    public class Stat
        //    {
        //        public string? name { get; set; }
        //        public string? url { get; set; }
        //    }

        //    public class TypeGroup
        //    {
        //        public int? slot { get; set; }
        //        public Type type { get; set; }
        //    }
        //    public class Type
        //    {
        //        public string? name { get; set; }
        //        public string? url { get; set; }
        //    }

        //    public class MoveGroup
        //    {
        //        public Move move { get; set; }
        //    }
        //    public class Move
        //    {
        //        public string? name { get; set; }
        //        public string? url { get; set; }
        //    }

        //    public class Sprites
        //    {
        //        public string? front_default { get; set; }
        //        public string? back_default { get; set; }
        //    }
        //}
    }
}