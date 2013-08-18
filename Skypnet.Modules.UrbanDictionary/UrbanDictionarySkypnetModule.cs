// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UrbanDictionarySkypnetModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   Defines the UrbanDictionarySkypnetModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.UrbanDictionary
{
    using System;
    using System.Text.RegularExpressions;
    using Ninject;
    using RestSharp.Extensions;
    using SKYPE4COMLib;
    using Skypnet.Core;
    using Skypnet.Modules.UrbanDictionary.Services;

    /// <summary>
    /// The Urban dictionary Skypnet module.
    /// </summary>
    public class UrbanDictionarySkypnetModule : AbstractSkypnetModule
    {
        /// <summary>
        /// The regex pattern.
        /// </summary>
        private const string RegexPattern = @"^!urba(nu|n)\s+(.*)";

        /// <summary>
        /// Precompile our matcher.
        /// </summary>
        private static readonly Regex UrbanRegex = new Regex(RegexPattern, RegexOptions.Compiled);

        /// <summary>
        /// Gets or sets the urban service.
        /// </summary>
        [Inject]
        public IUrbanService UrbanService { get; set; }

        #region Overrides of AbstractSkypnetModule

        /// <summary>Registers the event handlers of the module.</summary>
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
                Match match = UrbanRegex.Match(chatMessage.Body);

                if (match.Success)
                {
                    bool urlRequested = match.Groups[1].Value.Equals("nu", StringComparison.InvariantCultureIgnoreCase);
                    string term = match.Groups[2].Value;

                    UrbanResponse urbanResponse = this.UrbanService.Search(new UrbanRequest(term));

                    if (urbanResponse != null)
                    {
                        if (urbanResponse.Definition.HasValue())
                        {
                            string definition = "Definition: " + urbanResponse.Definition.Replace('\r', ' ').Replace('\n', ' ');
                            chatMessage.Chat.SendMessage(definition);
                        }

                        if (urbanResponse.Example.HasValue())
                        {
                            string example = "Example: " + urbanResponse.Example.Replace('\r', ' ').Replace('\n', ' ');
                            chatMessage.Chat.SendMessage(example);
                        }

                        if (urlRequested && urbanResponse.Url.HasValue())
                        {
                            string url = "Url: " + urbanResponse.Url;
                            chatMessage.Chat.SendMessage(url);
                        }
                    }
                    else
                    {
                        chatMessage.Chat.SendMessage(string.Format("Nothing for term '{0}' was found.", term));
                    }
                }
            }
        }

        #endregion
    }
}
