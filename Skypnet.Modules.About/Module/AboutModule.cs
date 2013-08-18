// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AboutModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The about module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.About.Module
{
    using Ninject.Modules;
    using Skypnet.Core;

    /// <summary>
    /// The about module.
    /// </summary>
    public class AboutModule : NinjectModule
    {
        /// <summary>
        /// Loads any bindings configured into the Ninject Kernel.
        /// </summary>
        public override void Load()
        {
            Bind<ISkypnetModule>()
                .To<AboutSkypnetModule>()
                .WithPropertyValue("Name", "About")
                .WithPropertyValue("Description", "Module which gives information about skypnet")
                .WithPropertyValue("Instructions", "!about");
        }
    }
}