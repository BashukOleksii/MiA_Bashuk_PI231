using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class Country
{
    public Name name { get; set; }
    public List<string> capital { get; set; }
    public string region { get; set; }
    public double area { get; set; }
    public long population { get; set; }
}
public class Name
{
    public string common { get; set; }
    public string official { get; set; }
}
class Progrm
{
    private static readonly HttpClient _http = new HttpClient
    {
        BaseAddress = new Uri("https://restcountries.com"),
        Timeout = TimeSpan.FromSeconds(15)
    };
    static async Task Main()
    {
        try
        {
            Console.WriteLine("Запит до REST Countries...");
            var response = await _http.GetAsync("/v3.1/name/ukraine");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine("\nRaw JSON:");
            Console.WriteLine(json);
            var country = JsonSerializer.Deserialize<List<Country>>(json);
            if (country != null)
            {
                Console.WriteLine("\n ===== Результат парсингу =====");
                Console.WriteLine($"Country common name: {country[0].name.common}");
                Console.WriteLine($"Country official name: {country[0].name.official}");
                Console.WriteLine($"Country capital: {country[0].capital[0]}");
                Console.WriteLine($"Country region: {country[0].region}");
                Console.WriteLine($"Country area: {country[0].area}");
                Console.WriteLine($"Country population: {country[0].population}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка " + ex.Message);
        }
    }
}
