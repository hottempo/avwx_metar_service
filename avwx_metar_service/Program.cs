using System;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.Graph.Models;
using System.Net.Http.Json;
using System.Text.Json;
using avwx_metar_service;


var builder = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);

IConfiguration configuration = builder.Build();

string apiPasscode = configuration["ApiPasscode"];
string apiUrl = configuration["ApiUrl"];
string responseBody = "";

using (HttpClient client = new HttpClient())
{
    try
    {
        Console.WriteLine("Querying the API...");
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiPasscode}");
        HttpResponseMessage response = await client.GetAsync(apiUrl);

        response.EnsureSuccessStatusCode(); // Throw if not a success code.
        responseBody = await response.Content.ReadAsStringAsync();

        Console.WriteLine("API Response:");
        Console.WriteLine(responseBody);
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine($"Request error: {e.Message}");
    }


    try
    {
        var response = JsonSerializer.Deserialize<Dictionary<string, Metar>>(responseBody);

        if (response != null)
        {
            foreach (var entry in response)
            {
                Console.WriteLine(entry.Value.WindDirection.Value);
            }
        }
    }
    catch (JsonException e)
    {
        Console.WriteLine($"JSON error: {e.Message}");
    }
}



