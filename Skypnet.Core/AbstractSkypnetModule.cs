using System;
using Ninject;
using SKYPE4COMLib;
using Skypnet.Client;

namespace Skypnet.Core
{
    public abstract class AbstractSkypnetModule : ISkypnetModule
    {
        /// <summary>
        /// Contains the list of loaded modules
        /// Optional, but useful for the About and Help Modules
        /// </summary>
        [Inject]
        public ModuleManager ModuleManager { get; set; }

        /// <summary>
        /// Contains the Skype COM instance
        /// This is required for most modules that depend
        /// on communication between the Skype client
        /// </summary>
        [Inject]
        public SkypeContainer SkypeContainer { get; set; }

        /// <summary>
        /// Gets or Sets the name of the module.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or Sets the description of the module.
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets of Sets the Instructions of the module.
        /// </summary>
        public virtual string Instructions { get; set; }

        /// <summary>
        /// Registers the event handlers of the module.
        /// </summary>
        public abstract void RegisterEventHandlers();
    }
}