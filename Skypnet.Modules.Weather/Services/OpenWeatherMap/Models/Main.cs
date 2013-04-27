namespace Skypnet.Modules.Weather.OpenWeatherMap.Models
{
    public class Main
    {
        public double Temp { get; set; }
        public double Humidity { get; set; }
        public int Pressure { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public double SeaLevel { get; set; }
        public double GrndLevel { get; set; }
        public double TempKf { get; set; }
    }
}