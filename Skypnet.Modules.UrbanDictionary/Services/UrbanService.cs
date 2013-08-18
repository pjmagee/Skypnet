// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UrbanService.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The urban service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.UrbanDictionary.Services
{
    using RestSharp;
    using RestSharp.Deserializers;

    /// <summary>
    /// The urban service.
    /// </summary>
    public class UrbanService : IUrbanService
    {
        /// <summary>
        /// The base url.
        /// </summary>
        private const string BaseUrl = "http://urbanscraper.herokuapp.com/";

        /// <summary>The request to execute.</summary>
        /// <param name="request">The request.</param>
        /// <typeparam name="T">The T to deserialize to.</typeparam>
        /// <returns>The <see cref="T"/>.</returns>
        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient() { BaseUrl = BaseUrl };

            // hack this so that it handles it as text and force it use the JsonDeserializer
            client.ClearHandlers();
            client.AddHandler("text/plain", new JsonDeserializer());

            var restResponse = client.Execute<T>(request);
            return restResponse.Data;
        }

        /// <summary>The search to perform.</summary>
        /// <param name="urbanRequest">The urban request.</param>
        /// <returns>The <see cref="UrbanResponse"/>.</returns>
        public UrbanResponse Search(UrbanRequest urbanRequest)
        {
            var request = new RestRequest { Resource = "define/{term}.json", RequestFormat = DataFormat.Json };
            request.AddParameter("term", urbanRequest.Term, ParameterType.UrlSegment);
            return this.Execute<UrbanResponse>(request);
        }
    }
}