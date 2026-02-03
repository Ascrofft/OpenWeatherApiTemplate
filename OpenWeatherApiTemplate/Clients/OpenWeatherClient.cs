using Microsoft.Extensions.Configuration;

namespace OpenWeatherApiTemplate.Clients;

public class OpenWeatherClient
{
    // TEST functie - simuleert een API call
    public Task<string> TestPingAsync(string city)
    {
        // HINT: hier zou normaal een HttpClient call komen
        return Task.FromResult($"[Client] API zou aangeroepen worden voor stad: {city}");
    }
}
