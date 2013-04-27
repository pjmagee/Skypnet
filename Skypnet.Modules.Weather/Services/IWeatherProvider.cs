namespace Skypnet.Modules.Weather.Services
{
    public interface IWeatherProvider
    {
        /// <summary>
        /// The Api Key used
        /// </summary>
        string ApiKey { get; set; }

        /// <summary>
        /// The instructions this weather provider supports
        /// </summary>
        /// <returns>The commands available</returns>
        string Instructions { get; set; }

        /// <summary>
        /// The weather information based on a request
        /// </summary>
        /// <param name="request">The weather information</param>
        /// <returns></returns>
        string GetWeather(string request);
    }
}