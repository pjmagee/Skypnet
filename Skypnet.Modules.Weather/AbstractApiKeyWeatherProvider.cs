namespace Skypnet.Modules.Weather
{
    public abstract class AbstractApiKeyWeatherProvider : IWeatherProvider
    {
        // Inject this
        public string ApiKey { get; set; }

        public abstract string GetWeather(string request);
    }
}