// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SkypeContainer.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The SkypeContainer is passed to all module instances
//   so that the same Skype interface instance is accessed
//   to allow modules to register their event handlers
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Client
{
    using SKYPE4COMLib;

    /// <summary>
    /// The SkypeContainer is passed to all module instances so that the same Skype interface instance is accessed to allow modules to register their event handlers. 
    /// </summary>
    public class SkypeContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SkypeContainer"/> class.
        /// </summary>
        public SkypeContainer()
        {
            Skype = new Skype();
            Skype.Attach(Protocol: 8, Wait: false);
        }

        /// <summary>
        /// Gets or sets Skype Interface COM.
        /// There is only ever one instance.
        /// </summary>
        public Skype Skype { get; set; }
    }
}