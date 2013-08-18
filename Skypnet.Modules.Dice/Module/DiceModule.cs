// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiceModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The dice module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.Dice.Module
{
    using Ninject.Modules;
    using Skypnet.Core;

    /// <summary>
    /// The dice module.
    /// </summary>
    public class DiceModule : NinjectModule
    {
        /// <summary>
        /// Loads any bindings configured into the Ninject Kernel.
        /// </summary>
        public override void Load()
        {
            Bind<ISkypnetModule>()
                .To<DiceSkypnetModule>()
                .WithPropertyValue("Name", "Dice")
                .WithPropertyValue("Description", "Rolls a 6 sided die. Fun times!")
                .WithPropertyValue("Instructions", "!roll [optional number of times]");
        }
    }
}