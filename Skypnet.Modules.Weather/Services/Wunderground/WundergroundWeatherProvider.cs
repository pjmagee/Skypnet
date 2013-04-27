using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using RestSharp;
using Skypnet.Modules.Weather.Wunderground.Models;

namespace Skypnet.Modules.Weather.Services.Wunderground
{
    public class WundergroundWeatherProvider : IWeatherProvider
    {
        private const string BaseUrl = "http://api.wunderground.com/";
        private const string RegexPattern = @"^!(?<command>w|fc)\s+(?<location>.*)";
        private static readonly Regex WeatherRegex = new Regex(RegexPattern, RegexOptions.Compiled);

        /// <summary>
        /// The Api Key used
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// The instructions this weather provider supports
        /// </summary>
        /// <returns>The commands available</returns>
        public string Instructions { get; set; }

        private T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient() {BaseUrl = BaseUrl};
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("key", ApiKey);

            var restResponse = client.Execute<T>(request);
            return restResponse.Data;
        }

        public string GetWeather(string request)
        {
            Match match = WeatherRegex.Match(request);

            if (match.Success)
            {
                string command = match.Groups["command"].Value;
                string location = match.Groups["location"].Value;

                var restRequest = new RestRequest
                {
                    Method = Method.GET,
                    Resource = "api/{key}/{resource}/lang:EN/q/{search}.json"
                };

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

                // hack the search because RestSharp encodes with UrlSegment
                restRequest.Resource = restRequest.Resource.Replace("{search}", location);

                var rootObject = Execute<RootObject>(restRequest);

                if (rootObject != null)
                {
                    if (rootObject.response != null)
                    {
                        // Request error
                        if (rootObject.response.error != null)
                        {
                            return rootObject.response.error.description;
                        }

                        // Ambigious search
                        if (rootObject.response.results != null)
                        {
                            var projection = rootObject.response.results.Select(x => new { x.city, x.country, x.zmw });
                            string response = "Search query ambigious. Possible locations:";
                            response += Environment.NewLine;
                            response += string.Join("; ", rootObject.response.results);
                            response += Environment.NewLine;
                            response += "Usage !" + command + " zmw:[code]";
                            return response;
                        }

                        // Weather conditions
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

                        // Weather forecast
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

                        // Satellite
                        if (rootObject.response.features.satellite == 1)
                        {
                            return "Satellite has not been implemented yet, sorry dude.";
                        }

                        // Astronomy
                        if (rootObject.response.features.astronomy == 1)
                        {
                            return "Astronomy has not been implemented yet, sorry dude.";
                        }
                    }
                }
            }

            return "Weather request syntax error.";

        }
    }
}