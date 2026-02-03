using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenWeatherApiTemplate.Clients;
using OpenWeatherApiTemplate.Services;

var configuration = new ConfigurationBuilder()
    // HINT: appsettings.json moet in je output folder staan -> check .csproj CopyToOutputDirectory
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    // HINT: user-secrets werken pas na: dotnet user-secrets init + set "OpenWeather:ApiKey"
    .AddUserSecrets<Program>()
    .Build();

var services = new ServiceCollection();

// Config beschikbaar maken via DI
services.AddSingleton<IConfiguration>(configuration);

services.AddHttpClient<OpenWeatherClient>(client =>
{
    // HINT: voorkomt eindeloos hangen
    // TODO: Haal TimeoutSeconds uit appsettings.json
    client.Timeout = TimeSpan.FromSeconds(10);
});

// Registreer services hier
services.AddSingleton<OpenWeatherClient>();
services.AddSingleton<WeatherService>();
// services.AddSingleton<SimpleMemoryCache>();

using var provider = services.BuildServiceProvider();

// --- Main loop (Opdracht 1) ---
// HINT: Program.cs blijft alleen flow + UI: menu tonen, input lezen, aanroepen van services.

while (true)
{
    // DEMO (Deze code kan je vervangen met jouw eigen implementatie)
    Console.Clear();
    Console.Write("Vul een stadsnaam in: ");
    var city = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(city))
    {
        Console.WriteLine("Stad mag niet leeg zijn.");
        Console.WriteLine("Druk op ENTER om door te gaan...");
        Console.ReadLine();
        continue;
    }

    var weatherService = provider.GetRequiredService<WeatherService>();

    // Dit is bewust alleen flow-test: Program -> Service -> Client (geen echte API call)
    var result = await weatherService.TestFlowAsync(city);

    Console.WriteLine();
    Console.WriteLine(result);
    Console.WriteLine();
    Console.WriteLine("Druk op ENTER om door te gaan...");
    Console.ReadLine();
    continue;
}
