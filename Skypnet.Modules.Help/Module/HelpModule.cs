// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HelpModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The help module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.Help.Module
{
    using Ninject.Modules;
    using Skypnet.Core;

    /// <summary>
    /// The help module.
    /// </summary>
    public class HelpModule : NinjectModule
    {
        /// <summary>
        /// Loads any bindings configured into the Ninject Kernel.
        /// </summary>
        public override void Load()
        {
            Bind<ISkypnetModule>()
                .To<HelpSkypnetModule>()
                .WithPropertyValue("Name", "Help")
                .WithPropertyValue("Description", "A module that returns information about modules.")
                .WithPropertyValue("Instructions", "!help [module] i.e '!help About' or '!help Weather' or just !help to bring back a list of modules");
        }
    }
}