// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RootObject.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The root object.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.YouTube.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The root object.
    /// </summary>
    public class RootObject
    {
        /// <summary>
        /// Gets or sets the kind.
        /// </summary>
        public string Kind { get; set; }

        /// <summary>
        /// Gets or sets the e-tag.
        /// </summary>
        public string Etag { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public List<Item> Items { get; set; }
    }
}