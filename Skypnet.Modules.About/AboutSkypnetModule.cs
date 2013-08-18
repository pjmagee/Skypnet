// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AboutSkypnetModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The about skypnet module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.About
{
    using System.Linq;
    using SKYPE4COMLib;
    using Skypnet.Core;

    /// <summary>
    /// The about skypnet module.
    /// </summary>
    public class AboutSkypnetModule : AbstractSkypnetModule
    {
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
                if (chatMessage.Body.Equals("!about"))
                {
                    chatMessage.Chat.SendMessage("This is skypnet bot.");
                    chatMessage.Chat.SendMessage("There are " + ModuleManager.Modules.Count() + " modules.");
                    chatMessage.Chat.SendMessage("Type !help to get more information about modules and how to use them.");
                }
            }
        }
    }
}
