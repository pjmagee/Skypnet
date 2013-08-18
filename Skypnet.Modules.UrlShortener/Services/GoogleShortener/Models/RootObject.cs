// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RootObject.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The root object.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.UrlShortener.Services.GoogleShortener.Models
{
    /*
     *  https://developers.google.com/url-shortener/v1/getting_started#shorten
     * 
     * {
     *   "kind": "urlshortener#url",
     *   "id": "http://goo.gl/fbsS",
     *   "longUrl": "http://www.google.com/"
     * }
     * 
     */

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
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the long url.
        /// </summary>
        public string LongUrl { get; set; }
    }
}
