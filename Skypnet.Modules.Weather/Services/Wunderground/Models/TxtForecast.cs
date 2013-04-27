using System.Collections.Generic;

namespace Skypnet.Modules.Weather.Wunderground.Models
{
    public class TxtForecast
    {
        public string date { get; set; }
        public List<Forecastday> forecastday { get; set; }
    }
}