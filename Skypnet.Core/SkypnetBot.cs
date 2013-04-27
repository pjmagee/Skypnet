using System;
using NLog;
using Ninject;
using SKYPE4COMLib;

namespace Skypnet.Core
{
    /// <summary>
    /// The skypnet bot engine.
    /// </summary>
    public class SkypnetBot
    {      
        public event EventHandler<BotStatusChangedEventArgs> BotStatusChanged;
        
        /// <summary>
        /// Defaults to Stopped Status
        /// </summary>
        private BotStatus botStatus = BotStatus.Stopped;


        private readonly ModuleManager moduleManager;

        public BotStatus BotStatus
        {
            get { return botStatus; }
            set
            {
                OnBotStatusChanged(botStatus, value);
                botStatus = value;
            }
        }

        [Inject]
        public SkypnetBot(ModuleManager moduleManager)
        {
            if(moduleManager == null)
                throw new ArgumentNullException("moduleManager");

            this.moduleManager = moduleManager;
        }

        public ModuleManager Manager { get { return moduleManager; } }

        public void Start()
        {
            if (BotStatus != BotStatus.Stopped)
            {
                throw new Exception(string.Format("Current BotStatus is : '{0}'. To start the bot the BotStatus must be 'Stopped'.", BotStatus));
            }
            
            BotStatus = BotStatus.Starting;

            // Register all the modules associated with this Manager
            //Manager.RegisterModules();
            moduleManager.RegisterModules();

            BotStatus = BotStatus.Started;
        }

        public void Stop()
        {
            BotStatus = BotStatus.Stopping;

            // Anything that needs to be done before stopping the bot
            // i.e saving configuration or finishing a job

            BotStatus = BotStatus.Stopped;
        }

        protected virtual void OnBotStatusChanged(BotStatus oldStatus, BotStatus newStatus)
        {
            if (BotStatusChanged != null)
                BotStatusChanged(this, new BotStatusChangedEventArgs(oldStatus, newStatus));
        }

    }
}