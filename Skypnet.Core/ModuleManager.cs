// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModuleManager.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   Defines the ModuleManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Core
{
    using System.Collections.Generic;
    using Ninject;
    using Skypnet.Common;

    /// <summary>
    /// The module manager.
    /// </summary>
    public class ModuleManager
    {
        /// <summary>
        /// Gets or sets the modules.
        /// </summary>
        [Inject]
        public IEnumerable<ISkypnetModule> Modules { get; set; }

        /// <summary>
        /// Register modules.
        /// </summary>
        public void RegisterModules()
        {
            this.Modules.Do((item, idx) => item.RegisterEventHandlers());
        }
    }
}