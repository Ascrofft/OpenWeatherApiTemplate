using OpenWeatherApiTemplate.Clients;

namespace OpenWeatherApiTemplate.Services;

public class WeatherService
{
    private readonly OpenWeatherClient _client;

    public WeatherService(OpenWeatherClient client)
    {
        _client = client;
    }

    // TEST functie - simuleert business logic laag
    public async Task<string> TestFlowAsync(string city)
    {
        var clientResult = await _client.TestPingAsync(city);

        // HINT: hier zou normaal vertaling van API response naar model plaatsviniden
        return $"[Service] Resultaat ontvangen:\n{clientResult}";
    }
}
