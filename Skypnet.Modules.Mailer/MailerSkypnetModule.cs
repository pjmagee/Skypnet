// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MailerSkypnetModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The mailer skypnet module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.Mailer
{
    using SKYPE4COMLib;
    using Skypnet.Core;

    /// <summary>
    /// The mailer skypnet module.
    /// </summary>
    public class MailerSkypnetModule : AbstractSkypnetModule
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
            SkypeContainer.Skype.MessageStatus += this.SkypeOnMessageStatus;
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
            if (chatMessage.Body.StartsWith("!mail"))
            {
                chatMessage.Chat.SendMessage("mailer called.");
            }
        }
    }
}