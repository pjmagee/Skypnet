﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IYouTubeProvider.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The YouTubeProvider interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.YouTube
{
    /// <summary>
    /// The YouTubeProvider interface.
    /// </summary>
    public interface IYouTubeProvider
    {
        /// <summary>
        /// Gets or sets the Api key.
        /// </summary>
        string ApiKey { get; set; }

        /// <summary>
        /// The get video information.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetVideoInformation(string id);
    }
}