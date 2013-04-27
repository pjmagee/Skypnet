using System.Globalization;
using System.Text.RegularExpressions;
using Ninject;
using SKYPE4COMLib;
using Skypnet.Core;

namespace Skypnet.Modules.Ascii
{
    public class AsciiSkypnetModule : AbstractSkypnetModule
    {
        private const string RegexPattern = @"(?<command>!ascii)\s+(?<emote>\S+)";
        private static readonly Regex Regex = new Regex(RegexPattern);

        /// <summary>
        /// Allows for mulitple Ascii modules to be created
        /// </summary>
        public string Emote { get; set; }

        /// <summary>
        /// The ascii art container
        /// </summary>
        public string Art { get; set; }

        public override void RegisterEventHandlers()
        {
            SkypeContainer.Skype.MessageStatus += SkypeOnMessageStatus;
        }

        private void SkypeOnMessageStatus(ChatMessage pMessage, TChatMessageStatus status)
        {
            if (status == TChatMessageStatus.cmsReceived || status == TChatMessageStatus.cmsSent)
            {
                // The trigger word for this ascii emote
                if (pMessage.Body.StartsWith("!ascii", true, CultureInfo.InvariantCulture))
                {
                    var match = Regex.Match(pMessage.Body);

                    if (match.Success)
                    {
                        var emote = match.Groups["emote"].Value;

                        if (emote.ToLower().Equals(Emote.ToLower()))
                            pMessage.Chat.SendMessage(Art);
                    }
                }
            }
        }
    }
}