using System;
using System.Text.RegularExpressions;
using Ninject;
using SKYPE4COMLib;
using Skypnet.Core;
using Skypnet.Modules.UrlShortener.Services;

namespace Skypnet.Modules.UrlShortener
{
    public class UrlShortenerSkypnetModule : AbstractSkypnetModule
    {
        private readonly IUrlShortenerProvider urlShortenerProvider;
        private const string RegexPatternV2 = @"!(?<command>minify)(\s+(?<service>\S+?))?\s+(?<url>(?<protocol>(ht|f)tp(s?))\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*))";
        private static readonly Regex UrlRegex = new Regex(RegexPatternV2, RegexOptions.Compiled);

        /// <summary>
        /// The Trigger used for this url shortener provider
        /// </summary>
        public string Trigger { get; set; }

        [Inject]
        public UrlShortenerSkypnetModule(IUrlShortenerProvider urlShortenerProvider)
        {
            if (urlShortenerProvider == null)
                throw new ArgumentNullException("urlShortenerProvider");

            this.urlShortenerProvider = urlShortenerProvider;
        }

        public override void RegisterEventHandlers()
        {
            SkypeContainer.Skype.MessageStatus += SkypeOnMessageStatus;
        }

        private void SkypeOnMessageStatus(ChatMessage pMessage, TChatMessageStatus status)
        {
            if (status == TChatMessageStatus.cmsSent || status == TChatMessageStatus.cmsReceived)
            {
                Match match = UrlRegex.Match(pMessage.Body);

                if (match.Success)
                {
                    var url = match.Groups["url"].Value;
                    var trigger = match.Groups["service"].Value;
                    
                    // If the service matches
                    if (trigger.ToLower().Equals(Trigger.ToLower()))
                    {
                        string shorten = urlShortenerProvider.Shorten(url);
                        pMessage.Chat.SendMessage(shorten);
                    }
                }
            }
        }
    }
}
