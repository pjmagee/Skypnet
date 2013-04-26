using System.Text.RegularExpressions;
using Ninject;
using SKYPE4COMLib;
using Skypnet.Client;
using Skypnet.Core;

namespace Skypnet.Modules.Weather
{
    public class WeatherModule : IModule
    {
        /// <summary>
        /// Gets or Sets the name of the module.
        /// </summary>
        public string Name
        {
            get { return "Weather"; }
        }
        
        /// <summary>
        /// Gets or Sets the description of the module.
        /// </summary>
        public string Description
        {
            get { return "Retrieve weather information"; }
        }

        /// <summary>
        /// Gets or Sets the usage instructions of the module.
        /// </summary>
        public string Instructions
        {
            get { return WeatherProvider.GetCommands(); }
        }
        
        [Inject]
        public SkypeContainer SkypeContainer { get; set; }

        [Inject]
        public IWeatherProvider WeatherProvider { get; set; }

        private const string RegexPattern = @"^!(w|fc)\s+(.*)";

        private static readonly Regex WeatherRegex = new Regex(RegexPattern, RegexOptions.Compiled);

        public void RegisterEventHandlers()
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
                    string weather = WeatherProvider.GetWeather(pMessage.Body);
                    pMessage.Chat.SendMessage(weather);
                }
            }
        }
    }

    
}
