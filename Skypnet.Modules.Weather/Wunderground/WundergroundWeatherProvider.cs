using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using RestSharp;
using Skypnet.Modules.Weather.Wunderground.Models;

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

        public override string GetCommands()
        {
            return "Usage: !w [location] i.e '!w sm5 2ht' or '!w England/London'";
        }

        public override string GetWeather(string request)
        {
            Match match = WeatherRegex.Match(request);

            string command = match.Groups[1].Value;
            string location = match.Groups[2].Value;

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
                        string response = "Search query ambigious. Possible locations: \n";
                        response += string.Join("; ", rootObject.response.results);
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
            
            // If we got this far
            return "There was an error with the weather request.";
        }
    }
}