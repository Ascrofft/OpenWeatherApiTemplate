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

// TODO: registreer hier je services

using var provider = services.BuildServiceProvider();

// --- Main loop (Opdracht 1) ---
// HINT: Program.cs blijft alleen flow + UI: menu tonen, input lezen, aanroepen van services.

while (true)
{
    Console.Clear();
    Console.WriteLine("Hello World!");
    // TODO: Implementeer menu (Opdracht 1)

    break;
}

// --- Functies & Helpers ---
// TODO
