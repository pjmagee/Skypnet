using System.Collections.Generic;

namespace SpikeLite.Modules.Weather.JsonObjects
{
    public class TxtForecast
    {
        public string date { get; set; }
        public List<Forecastday> forecastday { get; set; }
    }
}