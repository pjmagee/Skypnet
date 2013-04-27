using System;
using System.Linq;
using Ninject;
using SKYPE4COMLib;
using Skypnet.Client;
using Skypnet.Core;

namespace Skypnet.Modules.Help
{
    /// <summary>
    /// Provides information about a given module
    /// </summary>
    public class HelpSkypnetModule : AbstractSkypnetModule
    {
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

                        ISkypnetModule skypnetModule = ModuleManager.Modules.FirstOrDefault(x => x.Name.Equals(moduleName, StringComparison.OrdinalIgnoreCase));
                    
                        if (skypnetModule != null)
                        {
                            SendHelpResponse(pMessage, skypnetModule);
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

        private void SendHelpResponse(ChatMessage pMessage, ISkypnetModule skypnetModule)
        {
            pMessage.Chat.SendMessage("Module Name: " + skypnetModule.Name);
            pMessage.Chat.SendMessage("Module Description: " + skypnetModule.Description);
            pMessage.Chat.SendMessage("Module Instructions: " + skypnetModule.Instructions);
        }

        private void SendModuleListResponse(ChatMessage pMessage)
        {
            pMessage.Chat.SendMessage("Modules: " + string.Join(", ", ModuleManager.Modules.Select(x => x.Name)));
        }
    }
}
