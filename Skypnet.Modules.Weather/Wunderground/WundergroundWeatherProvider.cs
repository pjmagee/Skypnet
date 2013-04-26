using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using RestSharp;
using SpikeLite.Modules.Weather.JsonObjects;

namespace Skypnet.Modules.Weather.Wunderground
{
    public class WundergroundWeatherProvider : AbstractApiKeyWeatherProvider
    {
        private const string BaseUrl = "http://api.wunderground.com/";

        private const string RegexPattern = @"^!(w|fc)\s+(.*)";

        /// <summary>
        /// Precompile our matcher.
        /// </summary>
        private static readonly Regex WeatherRegex = new Regex(RegexPattern, RegexOptions.Compiled);

        private T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient() {BaseUrl = BaseUrl};
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

            var restRequest = new RestRequest {Resource = "api/{key}/{resource}/lang:EN/q/{search}.json"};

            switch (command.ToLower())
            {
                case "w":
                    restRequest.AddUrlSegment("resource", "conditions");
                    break;
                case "fc":
                case "fcn":
                    restRequest.AddUrlSegment("resource", "forecast");
                    break;
                case "st":
                    restRequest.AddUrlSegment("resource", "satellite");
                    break;
                case "mf":
                    restRequest.AddUrlSegment("resource", "astronomy");
                    break;
                default:
                    restRequest.AddUrlSegment("resource", "conditions");
                    break;
            }

            restRequest.AddUrlSegment("search", location);
            

            var rootObject = Execute<RootObject>(restRequest);

            if (rootObject.response.error != null)
            {
                return rootObject.response.error.description;
            }

            if (rootObject.response.results != null)
            {
                string response = "Search query ambigious. Possible locations: \n";
                response += string.Join("; ", rootObject.response.results) + "\n";
                response += "Usage ~" + command + " zmw:[code]";
                return response;
            }

            if (rootObject.response.features.conditions == 1)
            {
                var conditions = rootObject.current_observation;

                return string.Format("{0}, {1}, Feels like {2}, Humidity {3}, Winds {4}",
                                     conditions.observation_location.full,
                                     conditions.observation_time,
                                     conditions.feelslike_string,
                                     conditions.relative_humidity,
                                     conditions.wind_string);

            }

            if (rootObject.response.features.forecast == 1)
            {
                var forecast = rootObject.forecast;
                var response = new List<string>();

                foreach (var fc in forecast.txt_forecast.forecastday)
                {
                    string day = string.Format(@"{0}: {1}", fc.title, fc.fcttext_metric);

                    if (fc.title.ToLower().Contains("night"))
                    {
                        if (command.Equals("fcn", StringComparison.InvariantCultureIgnoreCase))
                        {
                            response.Add(day);
                        }
                    }
                    else
                    {
                        response.Add(day);
                    }
                }

                return string.Join("\n", response);
            }

            if (rootObject.response.features.satellite == 1)
            {
                return "Satellite has not been implemented yet, sorry dude.";
            }

            if (rootObject.response.features.astronomy == 1)
            {
                return "Astronomy has not been implemented yet, sorry dude.";
            }

            return "Invalid Syntax";
        }
    }
}