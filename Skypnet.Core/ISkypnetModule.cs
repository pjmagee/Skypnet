// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISkypnetModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   A defined module of the skypnet bot engine.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Core
{
    /// <summary>
    /// A defined module of the skypnet bot engine.
    /// </summary>
    public interface ISkypnetModule
    {
        /// <summary>
        /// Gets or Sets the name of the module.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or Sets the description of the module.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets or Sets the usage instructions of the module.
        /// </summary>
        string Instructions { get; }

        /// <summary>
        /// Register the skype event handlers of the module.
        /// </summary>
        void RegisterEventHandlers();
    }
}