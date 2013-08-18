// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GoogleUrlProvider.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   Defines the GoogleUrlProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.UrlShortener.Services.GoogleShortener
{
    using RestSharp;
    using RestSharp.Serializers;

    using Skypnet.Modules.UrlShortener.Services.GoogleShortener.Models;

    /// <summary>
    /// The google url provider.
    /// </summary>
    public class GoogleUrlProvider : IUrlShortenProvider
    {
        /// <summary>
        /// The base url.
        /// </summary>
        private const string BaseUrl = "https://www.googleapis.com/";

        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        public string ApiKey { get; set; }
        
        /// <summary>
        /// Shortens a given url.
        /// <![CDATA[
        /// https://developers.google.com/url-shortener/v1/getting_started#shorten
        /// ]]>
        /// </summary>
        /// <param name="url">The url to shorten.</param>
        /// <returns>The shortened url as a <see cref="string"/>.</returns>
        public string Shorten(string url)
        {
            var request = new RestRequest() { Method = Method.POST, RequestFormat = DataFormat.Json, JsonSerializer = new JsonSerializer() };
            request.AddBody(new { longUrl = url });

            var rootObject = this.Execute<RootObject>(request);
            return rootObject.Id;
        }

        /// <summary>
        /// The request to execute.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <typeparam name="T">The Type to deserialize.</typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        private T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient(BaseUrl + "/urlshortener/v1/url?key=" + this.ApiKey);
            var restResponse = client.Execute<T>(request);
            return restResponse.Data;
        }
    }
}