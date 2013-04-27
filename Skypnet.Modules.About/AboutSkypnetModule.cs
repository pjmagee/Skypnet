using System.Linq;
using SKYPE4COMLib;
using Skypnet.Core;

namespace Skypnet.Modules.About
{
    public class AboutSkypnetModule : AbstractSkypnetModule
    {
        public override void RegisterEventHandlers()
        {
            SkypeContainer.Skype.MessageStatus += SkypeOnMessageStatus;
        }

        private void SkypeOnMessageStatus(ChatMessage pMessage, TChatMessageStatus status)
        {
            if (status == TChatMessageStatus.cmsSent || status == TChatMessageStatus.cmsReceived)
            {
                if (pMessage.Body.Equals("!about"))
                {
                    pMessage.Chat.SendMessage("This is skypnet bot.");
                    pMessage.Chat.SendMessage("There are " + ModuleManager.Modules.Count() + " modules.");
                    pMessage.Chat.SendMessage("Type !help to get more information about modules and how to use them.");
                }
            }
        }
    }
}
