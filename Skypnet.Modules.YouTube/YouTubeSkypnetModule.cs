// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YouTubeSkypnetModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   A YouTube service module that can interact with youtube using the youtube api v3
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.YouTube
{
    using System;
    using System.Text.RegularExpressions;
    using Ninject;
    using SKYPE4COMLib;
    using Skypnet.Core;

    /// <summary>
    /// A YouTube service module that can interact with youtube using the youtube api v3
    /// </summary>
    public class YouTubeSkypnetModule : AbstractSkypnetModule
    {
        /// <summary>
        /// The regex pattern.
        /// </summary>
        private const string RegexPattern = @"http://(?:www\.)?youtu(?:be\.com/watch\?v=|\.be/)(?<id>[\w-]*)(&(amp;)?[\w\?=]*)?";

        /// <summary>
        /// The you tube regex.
        /// </summary>
        private static readonly Regex YouTubeRegex = new Regex(RegexPattern, RegexOptions.Compiled);

        /// <summary>
        /// The you tube provider.
        /// </summary>
        private readonly IYouTubeProvider youTubeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="YouTubeSkypnetModule"/> class.
        /// </summary>
        /// <param name="youTubeProvider">
        /// The you tube provider.
        /// </param>
        /// <exception cref="ArgumentNullException">Throws if the argument is null.
        /// </exception>
        [Inject]
        public YouTubeSkypnetModule(IYouTubeProvider youTubeProvider)
        {
            if (youTubeProvider == null)
            {
                throw new ArgumentNullException("youTubeProvider");
            }

            this.youTubeProvider = youTubeProvider;
        }

        /// <summary>
        /// Registers the event handlers of the module.
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
                Match expressionMatch = YouTubeRegex.Match(chatMessage.Body);

                if (expressionMatch.Success)
                {
                    string id = expressionMatch.Groups["id"].Value;
                    string videoInformation = this.youTubeProvider.GetVideoInformation(id);
                    chatMessage.Chat.SendMessage(videoInformation);
                }
            }
        }
    }
}
