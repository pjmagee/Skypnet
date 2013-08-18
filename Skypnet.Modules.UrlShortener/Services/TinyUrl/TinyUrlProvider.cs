// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TinyUrlProvider.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The tiny url provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.UrlShortener.Services.TinyUrl
{
    using System.Net.Http;

    /// <summary>
    /// The tiny url provider.
    /// </summary>
    public class TinyUrlProvider : IUrlShortenProvider
    {
        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Shortens a given url.
        /// </summary>
        /// <param name="url">
        /// The url to shorten.
        /// </param>
        /// <returns>
        /// The shortened url as a <see cref="string"/>.
        /// </returns>
        public string Shorten(string url)
        {
            var httpClient = new HttpClient();
            var result = httpClient.GetAsync("http://tinyurl.com/api-create.php?url=" + url).Result;
            return result.Content.ReadAsStringAsync().Result;
        }
    }
}
