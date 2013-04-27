using System;
using System.Text.RegularExpressions;
using Ninject;
using SKYPE4COMLib;
using Skypnet.Core;

namespace Skypnet.Modules.YouTube
{
    /// <summary>
    /// A YouTube service module that can interact with youtube using the youtube api v3
    /// </summary>
    public class YouTubeSkypnetModule : AbstractSkypnetModule
    {
        private const string RegexPattern = @"http://(?:www\.)?youtu(?:be\.com/watch\?v=|\.be/)(?<id>[\w-]*)(&(amp;)?[\w\?=]*)?";
        private static readonly Regex YouTubeRegex = new Regex(RegexPattern, RegexOptions.Compiled);
        private readonly IYouTubeProvider youTubeProvider;

        [Inject]
        public YouTubeSkypnetModule(IYouTubeProvider youTubeProvider)
        {
            if(youTubeProvider == null)
                throw new ArgumentNullException("youTubeProvider");

            this.youTubeProvider = youTubeProvider;
        }

        /// <summary>
        /// Registers the event handlers of the module.
        /// </summary>
        public override void RegisterEventHandlers()
        {
            SkypeContainer.Skype.MessageStatus += SkypeOnMessageStatus;
        }

        private void SkypeOnMessageStatus(ChatMessage pMessage, TChatMessageStatus status)
        {
            if (status == TChatMessageStatus.cmsReceived || status == TChatMessageStatus.cmsSent)
            {
                Match expressionMatch = YouTubeRegex.Match(pMessage.Body);

                if (expressionMatch.Success)
                {
                    string id = expressionMatch.Groups["id"].Value;
                    string videoInformation = youTubeProvider.GetVideoInformation(id);
                    pMessage.Chat.SendMessage(videoInformation);
                }
            }
        }
    }
}
