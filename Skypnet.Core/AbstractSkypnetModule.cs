// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AbstractSkypnetModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The abstract skypnet module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Core
{
    using System;
    using Ninject;
    using Skypnet.Client;

    /// <summary>
    /// The abstract skypnet module.
    /// </summary>
    public abstract class AbstractSkypnetModule : ISkypnetModule, IDisposable
    {
        /// <summary>
        /// Gets or sets the list of loaded modules.
        /// Optional, but useful for the About and Help Modules.
        /// </summary>
        [Inject]
        public ModuleManager ModuleManager { get; set; }

        /// <summary>
        /// Gets or sets the Skype COM instance.
        /// This is required for most modules that depend on communication between the Skype client.
        /// </summary>
        [Inject]
        public SkypeContainer SkypeContainer { get; set; }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the module.
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the Instructions of the module.
        /// </summary>
        public virtual string Instructions { get; set; }

        /// <summary>
        /// Registers the event handlers of the module.
        /// </summary>
        public abstract void RegisterEventHandlers();

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public abstract void Dispose();

        #endregion
    }
}