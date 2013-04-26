using System.Text.RegularExpressions;
using RestSharp;

namespace Skypnet.Modules.Weather.OpenWeatherMap
{
    public class OpenWeatherMapProvider : AbstractApiKeyWeatherProvider
    {
        private const string BaseUrl = "http://api.wunderground.com/";

        private const string RegexPattern = @"^!(w|fc)\s+(.*)";

        /// <summary>
        /// Precompile our matcher.
        /// </summary>
        private static readonly Regex WeatherRegex = new Regex(RegexPattern, RegexOptions.Compiled);

        private T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient() { BaseUrl = BaseUrl };
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("key", ApiKey);

            var restResponse = client.Execute<T>(request);
            return restResponse.Data;
        }


        public override string GetWeather(string request)
        {
            Match match = WeatherRegex.Match(request);

            string command = match.Groups[1].Value;
            string location = match.Groups[2].Value;


            // Code here
            throw new System.NotImplementedException();
        }
    }
}