// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Statistics.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The statistics.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.YouTube.Models
{
    /// <summary>
    /// The statistics.
    /// </summary>
    public class Statistics
    {
        /// <summary>
        /// Gets or sets the view count.
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// Gets or sets the like count.
        /// </summary>
        public int LikeCount { get; set; }

        /// <summary>
        /// Gets or sets the dislike count.
        /// </summary>
        public int DislikeCount { get; set; }

        /// <summary>
        /// Gets or sets the favorite count.
        /// </summary>
        public int FavoriteCount { get; set; }

        /// <summary>
        /// Gets or sets the comment count.
        /// </summary>
        public int CommentCount { get; set; }
    }
}