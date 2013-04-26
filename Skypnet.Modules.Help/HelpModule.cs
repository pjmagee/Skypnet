using System;
using System.Linq;
using Ninject;
using SKYPE4COMLib;
using Skypnet.Client;
using Skypnet.Core;

namespace Skypnet.Modules.Help
{
    public class HelpModule : ModuleBase
    {
        /// <summary>
        /// Gets or Sets the name of the module.
        /// </summary>
        public override string Name
        {
            get { return "Help"; }
        }

        /// <summary>
        /// Gets or Sets the description of the module.
        /// </summary>
        public override string Description
        {
            get { return "A module that gives information about modules"; }
        }

        /// <summary>
        /// Gets or Sets the usage instructions of the module.
        /// </summary>
        public override string Instructions
        {
            get { return "Usage: !help [module] i.e '!help About' or '!help Weather' or just !help to bring back a list of modules"; }
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
                string[] messageArray = pMessage.Body.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);

                if (messageArray.First().Equals("!help", StringComparison.OrdinalIgnoreCase))
                {
                    if (messageArray.Length == 1)
                    {
                        SendModuleListResponse(pMessage);
                    }
                    else if (messageArray.Length == 2)
                    {
                        string moduleName = messageArray.Last();

                        IModule module = ModuleManager.Modules.FirstOrDefault(x => x.Name.Equals(moduleName, StringComparison.OrdinalIgnoreCase));
                    
                        if (module != null)
                        {
                            SendHelpResponse(pMessage, module);
                        }
                    }
                    else
                    {
                        SendIncorrectSyntaxResponse(pMessage);
                        SendModuleListResponse(pMessage);
                    }
                }
            }
        }

        private void SendIncorrectSyntaxResponse(ChatMessage pMessage)
        {
            pMessage.Chat.SendMessage("Request was not in the correct syntax.");
        }

        private void SendHelpResponse(ChatMessage pMessage, IModule module)
        {
            pMessage.Chat.SendMessage("Module Name: " + module.Name);
            pMessage.Chat.SendMessage("Module Description: " + module.Description);
            pMessage.Chat.SendMessage("Module Instructions: " + module.Instructions);
        }

        private void SendModuleListResponse(ChatMessage pMessage)
        {
            pMessage.Chat.SendMessage("Modules: " + string.Join(", ", ModuleManager.Modules.Select(x => x.Name)));
        }
    }
}
