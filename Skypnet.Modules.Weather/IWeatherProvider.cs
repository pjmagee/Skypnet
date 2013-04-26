namespace Skypnet.Modules.Weather
{
    public interface IWeatherProvider
    {
        string GetWeather(string request);
    }
}