using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SKYPE4COMLib;
using Skypnet.Common;
using Skypnet.Core;

namespace Skypnet.Modules.Dice
{
    public class DiceSkypnetModule : AbstractSkypnetModule
    {
        private readonly Random random = new Random();
        private const string RegexPattern = @"^!((?<command>roll)(\s+(?<times>\d{1,2}))?)$";
        private static readonly Regex DiceRegex = new Regex(RegexPattern, RegexOptions.Compiled);

        public override void RegisterEventHandlers()
        {
            SkypeContainer.Skype.MessageStatus += SkypeOnMessageStatus;
        }

        private void SkypeOnMessageStatus(ChatMessage pMessage, TChatMessageStatus status)
        {
            if (status == TChatMessageStatus.cmsSent || status == TChatMessageStatus.cmsReceived)
            {
                Match match = DiceRegex.Match(pMessage.Body);

                if (match.Success)
                {
                    var group = match.Groups["times"];

                    if (group.Success)
                    {
                        int value = int.Parse(group.Value);
                        var results = new List<int>();
                        value.Times(() => results.Add(random.Next(1, 6)));
                        pMessage.Chat.SendMessage(string.Join(", ", results));
                    }
                    else
                    {
                        pMessage.Chat.SendMessage(random.Next(1, 6).ToString());
                    }
                }
            }
        }
    }
}
