using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using SKYPE4COMLib;
using Skypnet.Client;
using Skypnet.Core;

namespace Skypnet.Modules.About
{
    public class AboutModule : ModuleBase
    {
        public override string Name
        {
            get { return "About"; }
        }

        public override string Description
        {
            get { return "Module which gives information about skypnet"; }
        }

        public override string Instructions
        {
            get { return "Usage: !about"; }
        }

        [Inject]
        public SkypeContainer SkypeContainer { get; set; }

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
                    pMessage.Chat.SendMessage(pMessage.Sender.FullName + ", this is the skypnet bot.");
                }
            }
        }
    }
}
