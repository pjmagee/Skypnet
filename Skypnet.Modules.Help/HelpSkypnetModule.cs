// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HelpSkypnetModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   Provides information about a given module
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.Help
{
    using System;
    using System.Linq;
    using SKYPE4COMLib;
    using Skypnet.Core;

    /// <summary>
    /// Provides information about a given module
    /// </summary>
    public class HelpSkypnetModule : AbstractSkypnetModule
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
        /// The send incorrect syntax response.
        /// </summary>
        /// <param name="chatMessage">
        /// The p message.
        /// </param>
        private static void SendIncorrectSyntaxResponse(ChatMessage chatMessage)
        {
            chatMessage.Chat.SendMessage("Request was not in the correct syntax.");
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
            if (status == TChatMessageStatus.cmsSent || status == TChatMessageStatus.cmsReceived)
            {
                string[] messageArray = chatMessage.Body.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                if (messageArray.First().Equals("!help", StringComparison.OrdinalIgnoreCase))
                {
                    if (messageArray.Length == 1)
                    {
                        this.SendModuleListResponse(chatMessage);
                    }
                    else if (messageArray.Length == 2)
                    {
                        string moduleName = messageArray.Last();

                        ISkypnetModule skypnetModule = ModuleManager.Modules.FirstOrDefault(x => x.Name.Equals(moduleName, StringComparison.OrdinalIgnoreCase));
                    
                        if (skypnetModule != null)
                        {
                            this.SendHelpResponse(chatMessage, skypnetModule);
                        }
                    }
                    else
                    {
                        SendIncorrectSyntaxResponse(chatMessage);
                        this.SendModuleListResponse(chatMessage);
                    }
                }
            }
        }

        /// <summary>
        /// The send help response.
        /// </summary>
        /// <param name="chatMessage">
        /// The chatMessage.
        /// </param>
        /// <param name="skypnetModule">
        /// The skypnet module.
        /// </param>
        private void SendHelpResponse(ChatMessage chatMessage, ISkypnetModule skypnetModule)
        {
            chatMessage.Chat.SendMessage("Module Name: " + skypnetModule.Name);
            chatMessage.Chat.SendMessage("Module Description: " + skypnetModule.Description);
            chatMessage.Chat.SendMessage("Module Instructions: " + skypnetModule.Instructions);
        }

        /// <summary>
        /// The send module list response.
        /// </summary>
        /// <param name="chatMessage">
        /// The p message.
        /// </param>
        private void SendModuleListResponse(ChatMessage chatMessage)
        {
            chatMessage.Chat.SendMessage("Modules: " + string.Join(", ", ModuleManager.Modules.Select(x => x.Name)));
        }
    }
}
