using System;
using SKYPE4COMLib;
using Skypnet.Core;

namespace Skypnet.Modules.Mailer
{
    public class MailerSkypnetModule : AbstractSkypnetModule
    {
        public override void RegisterEventHandlers()
        {
            SkypeContainer.Skype.MessageStatus += SkypeOnMessageStatus;
        }

        private void SkypeOnMessageStatus(ChatMessage pMessage, TChatMessageStatus status)
        {
            if (pMessage.Body.StartsWith("!mail"))
                pMessage.Chat.SendMessage("mailer called.");
        }
    }
}