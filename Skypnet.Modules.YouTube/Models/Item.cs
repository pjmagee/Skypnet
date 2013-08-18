// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Item.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The item.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.YouTube.Models
{
    /// <summary>
    /// The item.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the kind.
        /// </summary>
        public string Kind { get; set; }

        /// <summary>
        /// Gets or sets the e-tag.
        /// </summary>
        public string Etag { get; set; }

        /// <summary>
        /// Gets or sets the snippet.
        /// </summary>
        public Snippet Snippet { get; set; }

        /// <summary>
        /// Gets or sets the content details.
        /// </summary>
        public ContentDetails ContentDetails { get; set; }

        /// <summary>
        /// Gets or sets the statistics.
        /// </summary>
        public Statistics Statistics { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public Status Status { get; set; }
    }
}