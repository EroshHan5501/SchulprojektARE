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
            var bulba= JsonSerializer.Deserialize<Results>(responseBody);
            Console.WriteLine("***** ***** *****");
            foreach(Pokemon pokemon in bulba.results) 
            {
                Console.WriteLine(pokemon.name);
                Console.WriteLine(pokemon.url);
            }
            Console.WriteLine(bulba.results);
            Console.WriteLine("***** ***** *****");
        }

        //Testing:
        public class Pages
        {
            public string version { get; set; }
        }

        //Actual:
        public class Pokemon
        {
            public string name { get; set; }
            public string url { get; set; }
            public List<Moves> moves { get; set; } 
        }
        public class Results
        {
            public List<Pokemon> results { get; set; }
        }
        public class Moves
        {

        }
    }
}