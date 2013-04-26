namespace Skypnet.Modules.Weather
{
    public interface IWeatherProvider
    {
        string GetCommands();
        string GetWeather(string request);
    }
}