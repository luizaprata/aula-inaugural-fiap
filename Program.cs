using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    static readonly HttpClient client = new HttpClient();
    static readonly Random random = new Random();

    static void Main()
    {
        RunAsync().GetAwaiter().GetResult();
    }

    static async Task RunAsync()
    {
        string key = GenerateKey();
        await MakePostRequest(key);
    }

    static async Task MakePostRequest(string key)
    {
        var payload = new { Key = key, grupo = 24 };
        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync("https://fiap-inaugural.azurewebsites.net/flap", content);
        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseBody);
    }

    static string GenerateKey()
    {
        int number = random.Next(1, 101);
        string letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZàáâãäåæçèéêëìíîïðñòóôõöùúûüýþÿ";
        return $"{letters[random.Next(letters.Length)]}{number}{letters[random.Next(letters.Length)]}";
    }

    
}
