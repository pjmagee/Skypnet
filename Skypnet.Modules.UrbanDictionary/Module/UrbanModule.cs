// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UrbanModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The urban module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.UrbanDictionary.Module
{
    using Ninject.Modules;
    using Skypnet.Core;
    using Skypnet.Modules.UrbanDictionary.Services;

    /// <summary>
    /// The urban module.
    /// </summary>
    public class UrbanModule : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<ISkypnetModule>()
                .To<UrbanDictionarySkypnetModule>()
                .WithPropertyValue("Name", "Urban Dictionary")
                .WithPropertyValue("Description", "Gets the first description for a given term.")
                .WithPropertyValue("Instructions", "!urban [term] i.e '!urban superman'");

            Bind<IUrbanService>().To<UrbanService>();
        }

        #endregion
    }
}
