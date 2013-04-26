using System.Collections.Generic;

namespace Skypnet.Modules.Weather.OpenWeatherMap.Models
{
    public class List
    {
        public int Dt { get; set; }
        public Main Main { get; set; }
        public List<Weather> Weather { get; set; }
        public Clouds Clouds { get; set; }
        public Wind Wind { get; set; }
        public Sys Sys { get; set; }
        public string DtTxt { get; set; }
        public Rain Rain { get; set; }
    }
}