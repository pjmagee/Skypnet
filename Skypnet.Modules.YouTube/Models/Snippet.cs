// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Snippet.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   Defines the Snippet type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.YouTube.Models
{
    /// <summary>
    /// The snippet.
    /// </summary>
    public class Snippet
    {
        /// <summary>
        /// Gets or sets the published at.
        /// </summary>
        public string PublishedAt { get; set; }

        /// <summary>
        /// Gets or sets the channel id.
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the thumbnails.
        /// </summary>
        public Thumbnails Thumbnails { get; set; }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        public string CategoryId { get; set; }
    }
}