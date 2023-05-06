namespace ApiCaller
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = String.Empty;
            string apiString = "https://pokeapi.co/api/v2/";
            if (args.Length > 0)
            {
                input = args[0];
            }

            HttpClient client = new HttpClient();
            using HttpResponseMessage response = client.GetAsync($"{apiString}{input}").Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(responseBody);
        }
    }
}