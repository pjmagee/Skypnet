using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ninject;
using SKYPE4COMLib;
using Skypnet.Client;
using Skypnet.Core;

namespace Skypnet.Modules.UrlShortener
{
    public class UrlShortenerModule : ModuleBase
    {
        public override string Name
        {
            get { return "UrlShortner"; }
        }

        public override string Description
        {
            get { return "Produces a short url to use by providing a url."; }
        }

        public override string Instructions
        {
            get { return "Usage: !tiny [url] i.e '!tiny http://google.com'"; }
        }

        private readonly SkypeContainer skypeContainer;
        private readonly IUrlShortenerProvider urlShortenerProvider;
        
        private const string RegexPattern = @"^!(?<command>tiny)\s+(?<url>(?<protocol>(ht|f)tp(s?))\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*))";
        private static readonly Regex UrlRegex = new Regex(RegexPattern, RegexOptions.Compiled);

        [Inject]
        public UrlShortenerModule(SkypeContainer skypeContainer, IUrlShortenerProvider urlShortenerProvider)
        {
            if(skypeContainer == null)
                throw new ArgumentNullException("skypeContainer");

            if(urlShortenerProvider == null)
                throw new ArgumentNullException("urlShortenerProvider");

            this.skypeContainer = skypeContainer;
            this.urlShortenerProvider = urlShortenerProvider;
        }

        public override void RegisterEventHandlers()
        {
            skypeContainer.Skype.MessageStatus += SkypeOnMessageStatus;
        }

        private void SkypeOnMessageStatus(ChatMessage pMessage, TChatMessageStatus status)
        {
            if (status == TChatMessageStatus.cmsSent || status == TChatMessageStatus.cmsReceived)
            {
                Match match = UrlRegex.Match(pMessage.Body);

                if (match.Success)
                {
                    var url = match.Groups["url"].Value;
                    string shorten = urlShortenerProvider.Shorten(url);
                    pMessage.Chat.SendMessage(shorten);
                }
            }
        }
    }
}
