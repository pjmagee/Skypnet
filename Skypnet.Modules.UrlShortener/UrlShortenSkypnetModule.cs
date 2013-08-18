// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UrlShortenSkypnetModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The url shortener skypnet module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.UrlShortener
{
    using System;
    using System.Text.RegularExpressions;
    using Ninject;
    using SKYPE4COMLib;
    using Skypnet.Core;
    using Skypnet.Modules.UrlShortener.Services;

    /// <summary>
    /// The url shorten skypnet module.
    /// </summary>
    public class UrlShortenSkypnetModule : AbstractSkypnetModule
    {
        /// <summary>
        /// The regex pattern v 2.
        /// </summary>
        private const string RegexPatternV2 = @"!(?<command>minify)(\s+(?<service>\S+?))?\s+(?<url>(?<protocol>(ht|f)tp(s?))\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*))";
        
        /// <summary>
        /// The url regex.
        /// </summary>
        private static readonly Regex UrlRegex = new Regex(RegexPatternV2, RegexOptions.Compiled);

        /// <summary>
        /// The url shorten provider.
        /// </summary>
        private readonly IUrlShortenProvider urlShortenProvider;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="UrlShortenSkypnetModule"/> class.
        /// </summary>
        /// <param name="urlShortenProvider">
        /// The url shorten provider.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null.</exception>
        [Inject]
        public UrlShortenSkypnetModule(IUrlShortenProvider urlShortenProvider)
        {
            if (urlShortenProvider == null)
            {
                throw new ArgumentNullException("urlShortenProvider");
            }

            this.urlShortenProvider = urlShortenProvider;
        }

        /// <summary>
        /// Gets or sets the Trigger used for this url shorten provider.
        /// </summary>
        public string Trigger { get; set; }

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
        /// The chat message.
        /// </param>
        /// <param name="status">
        /// The status.
        /// </param>
        private void SkypeOnMessageStatus(ChatMessage chatMessage, TChatMessageStatus status)
        {
            if (status == TChatMessageStatus.cmsSent || status == TChatMessageStatus.cmsReceived)
            {
                Match match = UrlRegex.Match(chatMessage.Body);

                if (match.Success)
                {
                    var url = match.Groups["url"].Value;
                    var trigger = match.Groups["service"].Value;
                    
                    // If the service matches
                    if (trigger.ToLower().Equals(this.Trigger.ToLower()))
                    {
                        string shorten = this.urlShortenProvider.Shorten(url);
                        chatMessage.Chat.SendMessage(shorten);
                    }
                }
            }
        }
    }
}
