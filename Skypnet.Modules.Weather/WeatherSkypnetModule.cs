// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WeatherSkypnetModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The weather skypnet module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.Weather
{
    using System;
    using System.Text.RegularExpressions;
    using Ninject;
    using SKYPE4COMLib;
    using Skypnet.Core;
    using Skypnet.Modules.Weather.Services;

    /// <summary>
    /// The weather Skypnet module.
    /// </summary>
    public class WeatherSkypnetModule : AbstractSkypnetModule
    {
        /// <summary>
        /// The regex pattern.
        /// </summary>
        private const string RegexPattern = @"^!(w|fc)\s+(.*)";

        /// <summary>
        /// The weather regex.
        /// </summary>
        private static readonly Regex WeatherRegex = new Regex(RegexPattern, RegexOptions.Compiled);

        /// <summary>
        /// The weather provider.
        /// </summary>
        private readonly IWeatherProvider weatherProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherSkypnetModule"/> class.
        /// </summary>
        /// <param name="weatherProvider">
        /// The weather provider.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if argument is null.
        /// </exception>
        [Inject]
        public WeatherSkypnetModule(IWeatherProvider weatherProvider)
        {
            if (weatherProvider == null)
            {
                throw new ArgumentNullException("weatherProvider");
            }

            this.weatherProvider = weatherProvider;
        }

        /// <summary>
        /// The register event handlers.
        /// </summary>
        public override void RegisterEventHandlers()
        {
            SkypeContainer.Skype.MessageStatus += this.SkypeOnMessageStatus;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            SkypeContainer.Skype.MessageStatus -= this.SkypeOnMessageStatus;
        }

        /// <summary>
        /// The skype on message status.
        /// </summary>
        /// <param name="chatMessage">
        /// The p message.
        /// </param>
        /// <param name="status">
        /// The status.
        /// </param>
        private void SkypeOnMessageStatus(ChatMessage chatMessage, TChatMessageStatus status)
        {
            if (status == TChatMessageStatus.cmsSent || status == TChatMessageStatus.cmsReceived)
            {
                Match match = WeatherRegex.Match(chatMessage.Body);
                
                if (match.Success)
                {
                    string weather = this.weatherProvider.GetWeather(chatMessage.Body);
                    chatMessage.Chat.SendMessage(weather);
                }
            }
        }
    }
}
