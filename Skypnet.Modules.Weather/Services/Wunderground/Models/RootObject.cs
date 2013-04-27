namespace Skypnet.Modules.Weather.Wunderground.Models
{
    public class RootObject
    {
        public Response response { get; set; }
        public CurrentObservation current_observation { get; set; }
        public Satellite satellite { get; set; }
        public Forecast forecast { get; set; }
        public MoonPhase moon_phase { get; set; }
    }
}