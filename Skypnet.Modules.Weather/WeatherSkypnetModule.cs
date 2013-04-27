using System;
using System.Text.RegularExpressions;
using Ninject;
using SKYPE4COMLib;
using Skypnet.Core;
using Skypnet.Modules.Weather.Services;

namespace Skypnet.Modules.Weather
{
    public class WeatherSkypnetModule : AbstractSkypnetModule
    {
        private readonly IWeatherProvider weatherProvider;
        private const string RegexPattern = @"^!(w|fc)\s+(.*)";
        private static readonly Regex WeatherRegex = new Regex(RegexPattern, RegexOptions.Compiled);

        [Inject]
        public WeatherSkypnetModule(IWeatherProvider weatherProvider)
        {
            if(weatherProvider == null)
                throw new ArgumentNullException("weatherProvider");

            this.weatherProvider = weatherProvider;
        }

        public override void RegisterEventHandlers()
        {
            SkypeContainer.Skype.MessageStatus += SkypeOnMessageStatus;
        }

        private void SkypeOnMessageStatus(ChatMessage pMessage, TChatMessageStatus status)
        {
            if (status == TChatMessageStatus.cmsSent || status == TChatMessageStatus.cmsReceived)
            {
                Match match = WeatherRegex.Match(pMessage.Body);
                
                if (match.Success)
                {
                    string weather = weatherProvider.GetWeather(pMessage.Body);
                    pMessage.Chat.SendMessage(weather);
                }
            }
        }
    }

    
}
