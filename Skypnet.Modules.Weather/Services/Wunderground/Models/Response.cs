using System.Collections.Generic;

namespace Skypnet.Modules.Weather.Wunderground.Models
{
    public class Response
    {
        public string version { get; set; }
        public string termsofService { get; set; }
        public Features features { get; set; }
        public Error error { get; set; }
        public List<Result> results { get; set; }
    }
}