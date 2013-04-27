using System.Collections.Generic;

namespace Skypnet.Modules.Weather.OpenWeatherMap.Models
{
    public class RootObject
    {
        public Coord Coord { get; set; }
        public Sys Sys { get; set; }
        public List<Weather> Weather { get; set; }
        public string Base { get; set; }
        public Main Main { get; set; }
        public Wind Wind { get; set; }
        public Clouds Clouds { get; set; }
        public int Dt { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cod { get; set; }
        public double Message { get; set; }
        public City City { get; set; }
        public int Cnt { get; set; }
        public List<List> List { get; set; }
    }
}