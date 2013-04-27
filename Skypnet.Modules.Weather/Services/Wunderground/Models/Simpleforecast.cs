using System.Collections.Generic;

namespace Skypnet.Modules.Weather.Wunderground.Models
{
    public class Simpleforecast
    {
        public List<Forecastday> forecastday { get; set; }
    }
}