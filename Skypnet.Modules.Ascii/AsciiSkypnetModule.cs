// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ASCIISkypnetModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   Defines the ASCIISkypnetModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.Ascii
{
    using System.Globalization;
    using System.Text.RegularExpressions;
    using SKYPE4COMLib;
    using Skypnet.Core;

    /// <summary>
    /// The ascii skypnet module.
    /// </summary>
    public class ASCIISkypnetModule : AbstractSkypnetModule
    {
        /// <summary>
        /// The regex pattern used to trigger ASCII commands.
        /// </summary>
        private const string RegexPattern = @"(?<command>!ascii)\s+(?<emote>\S+)";

        /// <summary>
        /// The regex. Compiled for performance. 
        /// </summary>
        private static readonly Regex Regex = new Regex(RegexPattern, RegexOptions.Compiled);

        /// <summary>
        /// Gets or sets the Emote
        /// Allows for multiple ascii modules to be created
        /// </summary>
        public string Emote { get; set; }

        /// <summary>
        /// Gets or sets the art
        /// The ascii art container
        /// </summary>
        public string Art { get; set; }

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
            if (status == TChatMessageStatus.cmsReceived || status == TChatMessageStatus.cmsSent)
            {
                // The trigger word for this ascii emote
                if (chatMessage.Body.StartsWith("!ascii", true, CultureInfo.InvariantCulture))
                {
                    var match = Regex.Match(chatMessage.Body);

                    if (match.Success)
                    {
                        var emote = match.Groups["emote"].Value;

                        if (emote.ToLower().Equals(this.Emote.ToLower()))
                        {
                            chatMessage.Chat.SendMessage(this.Art);
                        }
                    }
                }
            }
        }
    }
}