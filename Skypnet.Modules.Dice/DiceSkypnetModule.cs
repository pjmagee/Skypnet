// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiceSkypnetModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The dice skypnet module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.Dice
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using SKYPE4COMLib;
    using Skypnet.Common;
    using Skypnet.Core;

    /// <summary>
    /// The dice skypnet module.
    /// </summary>
    public class DiceSkypnetModule : AbstractSkypnetModule
    {
        /// <summary>
        /// The regex pattern.
        /// </summary>
        private const string RegexPattern = @"^!((?<command>roll)(\s+(?<times>\d{1,2}))?)$";

        /// <summary>
        /// The dice regex.
        /// </summary>
        private static readonly Regex DiceRegex = new Regex(RegexPattern, RegexOptions.Compiled);

        /// <summary>
        /// The random.
        /// </summary>
        private readonly Random random = new Random();

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
        /// The p message.
        /// </param>
        /// <param name="status">
        /// The status.
        /// </param>
        private void SkypeOnMessageStatus(ChatMessage chatMessage, TChatMessageStatus status)
        {
            if (status == TChatMessageStatus.cmsSent || status == TChatMessageStatus.cmsReceived)
            {
                Match match = DiceRegex.Match(chatMessage.Body);

                if (match.Success)
                {
                    var group = match.Groups["times"];

                    if (group.Success)
                    {
                        int value = int.Parse(group.Value);
                        var results = new List<int>();
                        value.Times(() => results.Add(this.random.Next(1, 6)));
                        chatMessage.Chat.SendMessage(string.Join(", ", results));
                    }
                    else
                    {
                        chatMessage.Chat.SendMessage(this.random.Next(1, 6).ToString(CultureInfo.InvariantCulture));
                    }
                }
            }
        }
    }
}
