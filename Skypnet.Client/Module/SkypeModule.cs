// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SkypeModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   Defines the SkypeModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Client.Module
{
    using Ninject.Modules;

    /// <summary>
    /// The skype module.
    /// </summary>
    public class SkypeModule : NinjectModule
    {
        /// <summary>
        /// The load.
        /// </summary>
        public override void Load()
        {
            Bind<SkypeContainer>().ToSelf().InSingletonScope();
        }
    }
}
