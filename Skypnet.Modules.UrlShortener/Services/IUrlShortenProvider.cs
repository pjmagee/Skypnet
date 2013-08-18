// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUrlShortenProvider.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The UrlShortenerProvider interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.UrlShortener.Services
{
    /// <summary>
    /// The UrlShortenerProvider interface.
    /// </summary>
    public interface IUrlShortenProvider
    {
        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        string ApiKey { get; set; }

        /// <summary>
        /// The shortened url.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The shortened url as a <see cref="string"/>.
        /// </returns>
        string Shorten(string url);
    }
}