namespace SpikeLite.Modules.Weather.JsonObjects
{
    public class MoonPhase
    {
        public string percentIlluminated { get; set; }
        public string ageOfMoon { get; set; }
        public CurrentTime current_time { get; set; }
        public Sunrise sunrise { get; set; }
        public Sunset sunset { get; set; }
    }
}