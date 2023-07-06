using MySqlConnector;
using Pokedex.Common;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
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

            using Pokedex.Common.DbTransition transition = new Pokedex.Common.DbTransition();

            #region Moves
            
            //Moves
            //Api request
            HttpClient clientProtoMove = new HttpClient();
            using HttpResponseMessage responseProtoMove = clientProtoMove.GetAsync($"{apiString}move?limit=918").Result;
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
                    transition.Insert(newMove);
                }
            }
            
            #endregion

            #region Abilities
            //Abilities
            //Api request
            HttpClient clientProtoAbility = new HttpClient();
            using HttpResponseMessage responseProtoAbility = clientProtoAbility.GetAsync($"{apiString}ability?limit=358").Result;
            string responseBodyProtoAbility = responseProtoAbility.Content.ReadAsStringAsync().Result;
            Console.WriteLine(responseBodyProtoAbility);
            ProtoList protoAbilityList = JsonSerializer.Deserialize<ProtoList>(responseBodyProtoAbility);
            if (protoAbilityList != null && protoAbilityList.results != null)
            {
                foreach (ProtoObject protoAbility in protoAbilityList.results)
                {
                    HttpClient newClient = new HttpClient();
                    using HttpResponseMessage newResponse = newClient.GetAsync(protoAbility.url).Result;
                    string newResponseBody = newResponse.Content.ReadAsStringAsync().Result;
                    Pokedex.Common.Ability newAbility = JsonSerializer.Deserialize<Pokedex.Common.Ability>(newResponseBody);
                    
                    foreach (Ability.EffectGroup effectGroup in newAbility.effect_entries)
                    {
                        if (effectGroup.language.name == "en")
                        {
                            newAbility.entry = StringCleaner(effectGroup.effect);
                            newAbility.shortEntry = StringCleaner(effectGroup.short_effect);
                        }
                    }
                    Console.WriteLine(newAbility.id);
                    Console.WriteLine(newAbility.name);
                    Console.WriteLine(newAbility.entry);
                    Console.WriteLine(newAbility.shortEntry);
                    transition.Insert(newAbility);
                }
            }
            #endregion

            #region Pokemon
            
            //Pokemon && PokeMoves && PokeAbilities
            //Api request
            HttpClient client = new HttpClient();
            using HttpResponseMessage response = client.GetAsync($"{apiString}{input}?limit=1281").Result;
            //using HttpResponseMessage response = client.GetAsync($"{apiString}{input}?limit=20&offset=898").Result;
            //using HttpResponseMessage response = client.GetAsync($"{apiString}pokemon/899").Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(responseBody);



            //Actual:
            ProtoList pokemonList = JsonSerializer.Deserialize<ProtoList>(responseBody);

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

                    //if (newPokemon.id == 899)
                    //{
                    //    Debugger.Launch();
                    //}
                    transition.Insert(newPokemon);
                    if (newSprites != null && newSprites.Front != null && newSprites.Back != null && newSprites.FPokemonId != null)
                    {
                        transition.Insert(newSprites);
                    }

                    foreach (Pokemon.MoveGroup moveGroup in newPokemon.moves)
                    {
                        Console.WriteLine(moveGroup.move.url);
                        Console.WriteLine(moveGroup.move.url.LastIndexOf("move/"));
                        Console.WriteLine(moveGroup.move.url.LastIndexOf('/'));
                        int startPos = moveGroup.move.url.LastIndexOf("move/") + 5;
                        int endPos = moveGroup.move.url.LastIndexOf('/');
                        int range = endPos - startPos;
                        Console.WriteLine(moveGroup.move.url.Substring(startPos, range));
                        int moveId = Convert.ToInt32(moveGroup.move.url.Substring(startPos, range));

                        PokeMove pokeMove = new PokeMove();
                        pokeMove.pokeId = newPokemon.id;
                        pokeMove.moveId = moveId;
                        transition.Insert(pokeMove);
                    }
                    foreach (Pokemon.AbilityGroup abilityGroup in newPokemon.abilities)
                    {
                        Console.WriteLine(abilityGroup.ability.url);
                        Console.WriteLine(abilityGroup.ability.url.LastIndexOf("ability/"));
                        Console.WriteLine(abilityGroup.ability.url.LastIndexOf('/'));
                        int startPos = abilityGroup.ability.url.LastIndexOf("ability/") + 8;
                        int endPos = abilityGroup.ability.url.LastIndexOf('/');
                        int range = endPos - startPos;
                        Console.WriteLine(abilityGroup.ability.url.Substring(startPos, range));
                        int abilityId = Convert.ToInt32(abilityGroup.ability.url.Substring(startPos, range));

                        PokeAbility pokeAbility = new PokeAbility();
                        pokeAbility.pokeId = newPokemon.id;
                        pokeAbility.abilityId = abilityId;
                        transition.Insert(pokeAbility);
                    }
                }
            }
            Console.WriteLine("***** ***** *****");
            
            #endregion

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

        public static string StringCleaner(string s)
        {
            if ((s.IndexOf('\'') >= 0))
            {
                return s.Remove(s.IndexOf('\''));
            }
            else
            {
                return s;
            }
        }
    }
}