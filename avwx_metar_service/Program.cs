using System;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using avwx_metar_service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);

IConfiguration configuration = builder.Build();

string connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("DefaultConnection");
string apiPasscode = configuration["ApiPasscode"] ?? throw new ArgumentNullException("ApiPasscode");
string apiUrl = configuration["ApiUrl"] ?? throw new ArgumentNullException("ApiUrl");
string apiUrl2 = configuration["ApiUrl2"] ?? throw new ArgumentNullException("ApiUrl2");

string responseBody = "";
string responseBody2 = "";

var serviceProvider = new ServiceCollection()
    .AddDbContext<AppDbContext>(options =>
        options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 31))))
    .AddSingleton<IConfiguration>(configuration)
    .BuildServiceProvider();

var dbContext = serviceProvider.GetRequiredService<AppDbContext>();


var rabbitMQService = new RabbitMQService("93.161.249.14", 5672, "metar_queue");

using (HttpClient client = new HttpClient())
{
    try
    {
        Console.WriteLine("Querying the API...");
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiPasscode}");

        // First API call
        HttpResponseMessage response = await client.GetAsync(apiUrl);
        response.EnsureSuccessStatusCode();
        responseBody = await response.Content.ReadAsStringAsync();

        // Second API call
        response = await client.GetAsync(apiUrl2);
        response.EnsureSuccessStatusCode();
        responseBody2 = await response.Content.ReadAsStringAsync();

    }
    catch (HttpRequestException e)
    {
        Console.WriteLine($"Request error: {e.Message}");
    }

    await SaveMetarsToDatabase(responseBody, dbContext);
    await SaveMetarsToDatabase(responseBody2, dbContext);
}

async Task SaveMetarsToDatabase(string responseBody, AppDbContext dbContext)
{
    var rabbitMQService = new RabbitMQService("93.161.249.14", 5672, "metar_queue");

    try
    {
        var metars = JsonSerializer.Deserialize<Dictionary<string, Metar>>(responseBody);

        if (metars != null)
        {
            foreach (var entry in metars)
            {
                var metar = entry.Value;

                // Check if the Metar already exists
                bool exists = await dbContext.Metars.AnyAsync(m =>
                    m.Station == metar.Station && m.Time.Dt == metar.Time.Dt);

                if (!exists)
                {
                    Console.WriteLine($"Adding new METAR: Station={metar.Station}, Time={metar.Time.Dt}");
                    dbContext.Metars.Add(metar);

                    // Publish the METAR object as a JSON message
                    rabbitMQService.PublishMessage(metar);
                }
                else
                {
                    Console.WriteLine($"Skipping duplicate METAR: Station={metar.Station}, Time={metar.Time.Dt}");
                }
            }

            // Save changes to the database
            await dbContext.SaveChangesAsync();
        }
    }
    catch (JsonException e)
    {
        Console.WriteLine($"JSON error: {e.Message}");
    }
}
