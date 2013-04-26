using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using SKYPE4COMLib;
using Skypnet.Client;
using Skypnet.Core;

namespace Skypnet.Modules.Dice
{
    public class DiceModule : ModuleBase
    {
        /// <summary>
        /// Gets or Sets the name of the module.
        /// </summary>
        public override string Name
        {
            get { return "Dice"; }
        }

        /// <summary>
        /// Gets or Sets the description of the module.
        /// </summary>
        public override string Description
        {
            get { return "Rolls a dice!"; }
        }

        /// <summary>
        /// Gets or Sets the usage instructions of the module.
        /// </summary>
        public override string Instructions
        {
            get { return "Usage: !roll"; }
        }

        [Inject]
        public SkypeContainer SkypeContainer { get; set; }

        /// <summary>
        /// The random generator to be used for the dice roll.
        /// </summary>
        private readonly Random random = new Random();

        public override void RegisterEventHandlers()
        {
            SkypeContainer.Skype.MessageStatus += SkypeOnMessageStatus;
        }

        private void SkypeOnMessageStatus(ChatMessage pMessage, TChatMessageStatus status)
        {
            if (status == TChatMessageStatus.cmsSent || status == TChatMessageStatus.cmsReceived)
            {
                if (pMessage.Body.Equals("!roll"))
                {
                    pMessage.Chat.SendMessage(random.Next(1, 6).ToString());
                }
            }
        }
    }
}
