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

namespace Skypnet.Modules.YouTube
{
    public class YouTubeModule : ModuleBase
    {
        public override string Name
        {
            get { return "YouTube"; }
        }

        public override string Description
        {
            get { return "Returns information about a given YouTube video link"; }
        }

        public override string Instructions
        {
            get { return "Paste a link to a youtube video or !yt [video id] i.e '!yt 130491'"; }
        }

        [Inject]
        public SkypeContainer SkypeContainer { get; set; }

        [Inject]
        public IYouTubeProvider YouTubeProvider { get; set; }

        // TODO: Implement regex behavior for identifying a youtube video

        private const string RegexPattern = @"http://(?:www\.)?youtu(?:be\.com/watch\?v=|\.be/)([\w-]*)(&(amp;)?[\w\?=]*)?";

        /// <summary>
        /// Precompile our matcher.
        /// </summary>
        private static readonly Regex YouTubeRegex = new Regex(RegexPattern, RegexOptions.Compiled);

        public override void RegisterEventHandlers()
        {
            SkypeContainer.Skype.MessageStatus += SkypeOnMessageStatus;
        }

        private void SkypeOnMessageStatus(ChatMessage pMessage, TChatMessageStatus status)
        {
            if (status == TChatMessageStatus.cmsReceived || status == TChatMessageStatus.cmsSent)
            {
                // TODO: Read up on using the YouTube api
                // TODO: Url: https://developers.google.com/youtube/v3/getting-started

                Match expressionMatch = YouTubeRegex.Match(pMessage.Body);

                if (expressionMatch.Success)
                {
                    string videoId = expressionMatch.Groups[1].Value;
                    string videoInformation = YouTubeProvider.GetVideoInformation(videoId);

                    pMessage.Chat.SendMessage(videoInformation);
                }
            }

        }
    }
}
