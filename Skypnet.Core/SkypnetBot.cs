// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SkypnetBot.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The skypnet bot engine.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Core
{
    using System;
    using Ninject;

    /// <summary>
    /// The skypnet bot engine.
    /// </summary>
    public class SkypnetBot
    {
        /// <summary>
        /// The module manager.
        /// </summary>
        private readonly ModuleManager moduleManager;

        /// <summary>
        /// Defaults to Stopped Status
        /// </summary>
        private BotStatus botStatus = BotStatus.Stopped;


        /// <summary>
        /// Initializes a new instance of the <see cref="SkypnetBot"/> class.
        /// </summary>
        /// <param name="moduleManager">
        /// The module manager.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the argument is null.
        /// </exception>
        [Inject]
        public SkypnetBot(ModuleManager moduleManager)
        {
            if (moduleManager == null)
            {
                throw new ArgumentNullException("moduleManager");
            }

            this.moduleManager = moduleManager;
        }

        /// <summary>
        /// The bot status changed.
        /// </summary>
        public event EventHandler<BotStatusChangedEventArgs> BotStatusChanged;

        /// <summary>
        /// Gets or sets the bot status.
        /// </summary>
        public BotStatus BotStatus
        {
            get
            {
                return this.botStatus;
            }

            set
            {
                this.OnBotStatusChanged(this.botStatus, value);
                this.botStatus = value;
            }
        }

        /// <summary>
        /// Gets the manager.
        /// </summary>
        public ModuleManager Manager
        {
            get { return this.moduleManager; }
        }

        /// <summary>
        /// The start.
        /// </summary>
        /// <exception cref="Exception">Throws an exception if the Bot is not currently stopped.
        /// </exception>
        public void Start()
        {
            if (BotStatus != BotStatus.Stopped)
            {
                throw new Exception(string.Format("Current BotStatus is : '{0}'. To start the bot the BotStatus must be 'Stopped'.", BotStatus));
            }
            
            BotStatus = BotStatus.Starting;
            this.moduleManager.RegisterModules();
            BotStatus = BotStatus.Started;
        }

        /// <summary>
        /// The stop.
        /// </summary>
        public void Stop()
        {
            BotStatus = BotStatus.Stopping;

            // Anything that needs to be done before stopping the bot i.e saving configuration or finishing a job
            BotStatus = BotStatus.Stopped;
        }

        /// <summary>
        /// The on bot status changed.
        /// </summary>
        /// <param name="oldStatus">
        /// The old status.
        /// </param>
        /// <param name="newStatus">
        /// The new status.
        /// </param>
        protected virtual void OnBotStatusChanged(BotStatus oldStatus, BotStatus newStatus)
        {
            if (this.BotStatusChanged != null)
            {
                this.BotStatusChanged(this, new BotStatusChangedEventArgs(oldStatus, newStatus));
            }
        }
    }
}