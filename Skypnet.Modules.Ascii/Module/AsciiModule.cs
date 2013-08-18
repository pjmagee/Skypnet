// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsciiModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   Defines the AsciiModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.Ascii.Module
{
    using Ninject.Modules;
    using Skypnet.Core;

    /// <summary>
    /// The ASCII module. 
    /// </summary>
    public class ASCIIModule : NinjectModule
    {
        /// <summary>
        /// Loads any bindings configured into the Ninject Kernel.
        /// </summary>
        public override void Load()
        {
            Bind<ISkypnetModule>()
                .To<ASCIISkypnetModule>()
                .WithPropertyValue("Name", "Ascii")
                .WithPropertyValue("Description", "Ascii art! Yaaaay ~")
                .WithPropertyValue("Instructions", "~ascii emote i.e '~ascii thumbsup'");
        }
    }
}
