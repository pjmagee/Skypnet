// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WeatherModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The weather module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.Weather.Module
{
    using System.ComponentModel;
    using Ninject.Modules;
    using Skypnet.Core;
    using Skypnet.Modules.Weather.Services;
    using Skypnet.Modules.Weather.Services.Wunderground;

    /// <summary>
    /// The weather module.
    /// </summary>
    public class WeatherModule : NinjectModule
    {
        /// <summary>
        /// The include open weather map.
        /// If true, the OpenWeatherMap implementation will be used, otherwise the default will be used.
        /// </summary>
        [Description("Third Party Open Weather Map API")]
        private readonly bool includeOpenWeatherMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherModule"/> class.
        /// </summary>
        /// <param name="includeOpenWeatherMap">
        /// The include open weather map. Defaults to false.
        /// </param>
        public WeatherModule(bool includeOpenWeatherMap = false)
        {
            this.includeOpenWeatherMap = includeOpenWeatherMap;
        }

        /// <summary>
        /// The load.
        /// </summary>
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

            if (this.includeOpenWeatherMap)
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
