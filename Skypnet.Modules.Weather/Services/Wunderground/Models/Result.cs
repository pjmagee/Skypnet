using System.Linq;

namespace Skypnet.Modules.Weather.Wunderground.Models
{
    public class Result
    {
        public string name { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string country_iso3166 { get; set; }
        public string country_name { get; set; }
        public string zmw { get; set; }
        public string l { get; set; }

        public override string ToString()
        {
            return city + ", " + state + ", " + country_name + ", " + l.Split('/').Last(); 
        }

    }
}