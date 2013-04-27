using System;
using Ninject.Modules;
using Skypnet.Core;
using Skypnet.Modules.Weather.Services;
using Skypnet.Modules.Weather.Services.Wunderground;

namespace Skypnet.Modules.Weather.Module
{
    public class WeatherModule : NinjectModule
    {
        private readonly bool includeOpenWeatherMap;

        public WeatherModule(bool includeOpenWeatherMap = false)
        {
            this.includeOpenWeatherMap = includeOpenWeatherMap;
        }

        public override void Load()
        {
            Bind<ISkypnetModule>()
                .To<WeatherSkypnetModule>()
                .Named("Wunderground")
                .WithPropertyValue("Name", "Wunderground")
                .WithPropertyValue("Description", "Provides all your weather information needs using the Wunderground service")
                .WithPropertyValue("Instructions", "!w [location] for the latest weather or !fc [location] for a 7 day forecast.");

            Bind<IWeatherProvider>()
                .To<WundergroundWeatherProvider>()
                .WhenParentNamed("Wunderground")
                .WithPropertyValue("ApiKey", "3ffac173009f680a");

            if (includeOpenWeatherMap)
            {
                Bind<ISkypnetModule>()
                    .To<WeatherSkypnetModule>()
                    .Named("OpenWeatherMap")
                    .WithPropertyValue("Name", "OpenWeatherMap")
                    .WithPropertyValue("Description", "Provides all your weather information needs using the Open Weather Map service")
                    .WithPropertyValue("Instructions", "Currently not supported.");
            }
        }
    }
}
