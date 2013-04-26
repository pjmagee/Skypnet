using System.Text.RegularExpressions;
using RestSharp;
using Skypnet.Modules.Weather.OpenWeatherMap.Models;

namespace Skypnet.Modules.Weather.OpenWeatherMap
{
    public class OpenWeatherMapProvider : AbstractApiKeyWeatherProvider
    {
        private const string BaseUrl = "http://api.openweathermap.org/";

        // TODO: Handle regex for open weather map commands
        private const string RegexPattern = @"^!(w|fc)\s+(.*)";

        /// <summary>
        /// Precompile our matcher.
        /// </summary>
        private static readonly Regex WeatherRegex = new Regex(RegexPattern, RegexOptions.Compiled);

        private T Execute<T>(RestRequest request) where T : new()
        {
            // http://openweathermap.org/appid

            var client = new RestClient() { BaseUrl = BaseUrl };
            request.RequestFormat = DataFormat.Json;

            // Add the following parameter to the GET request: APPID=APIKEY 
            // Example: http://openweathermap.org/data/2.3/forecast/city?id=524901&APPID=1111111111
            // OR add the following line to http header of request to the server: x-api-key:APIKEY

            request.AddHeader("x-api-key", ApiKey);
            request.AddUrlSegment("version", "2.5");

            var restResponse = client.Execute<T>(request);
            return restResponse.Data;
        }


        public override string GetCommands()
        {
            return "Usage: !w not implemented. Check back again soon.";
        }

        public override string GetWeather(string request)
        {
            // Code here

            Match match = WeatherRegex.Match(request);

            string command = match.Groups[1].Value;
            string location = match.Groups[2].Value;


            switch (command)
            {
                case "w":
                    return HandleWeatherRequest(location);
                case "fc":
                    return HandleForecastRequest(location);
            }
            
            return "There was an error with the weather request.";
        }

        /// <summary>
        /// Seaching by city name api.openweathermap.org/data/2.5/weather?q=London,uk
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        private string HandleWeatherRequest(string location)
        {
            RestRequest request = new RestRequest
                {
                    Resource = "data/{version}/weather",
                    Method = Method.GET
                };

            RootObject rootObject = Execute<RootObject>(request);

            if (rootObject != null)
            {
                // return response information
            }

            return "There was an error with the weather request.";
        }

        private string HandleForecastRequest(string location)
        {
            RestRequest request = new RestRequest
                {
                    Resource = "data/{version}/forecast",
                    Method = Method.GET
                };

            request.AddParameter("q", location, ParameterType.GetOrPost);

            RootObject rootObject = Execute<RootObject>(request);

            
            if (rootObject != null)
            {
                // TODO: extract forecast data and return response information
            }

            return "There was an error with the weather request.";
        }

    }
}