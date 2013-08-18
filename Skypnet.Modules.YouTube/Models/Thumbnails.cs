// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Thumbnails.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The thumbnails.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.YouTube.Models
{
    /// <summary>
    /// The thumbnails.
    /// </summary>
    public class Thumbnails
    {
        /// <summary>
        /// Gets or sets the default.
        /// </summary>
        public Default Default { get; set; }

        /// <summary>
        /// Gets or sets the medium.
        /// </summary>
        public Medium Medium { get; set; }

        /// <summary>
        /// Gets or sets the high.
        /// </summary>
        public High High { get; set; }
    }
}