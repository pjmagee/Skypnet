// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YouTubeProvider.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The you tube provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.YouTube
{
    using System.Linq;
    using RestSharp;
    using Skypnet.Modules.YouTube.Models;

    /// <summary>
    /// The you tube provider.
    /// </summary>
    public class YouTubeProvider : IYouTubeProvider
    {
        /// <summary>
        /// The base url.
        /// </summary>
        private const string BaseUrl = "https://www.googleapis.com/";
     
        /// <summary>
        /// Initializes a new instance of the <see cref="YouTubeProvider"/> class.
        /// </summary>
        public YouTubeProvider()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YouTubeProvider"/> class.
        /// </summary>
        /// <param name="apiKey">
        /// The API key.
        /// </param>
        public YouTubeProvider(string apiKey)
        {
            this.ApiKey = apiKey;
        }

        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// The get video information.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetVideoInformation(string id)
        {
            var request = new RestRequest { Method = Method.GET, Resource = "youtube/v3/videos" };

            request.AddParameter("id", id);
            request.AddParameter("part", "snippet,contentDetails,statistics,status");

            var rootObject = this.Execute<RootObject>(request);

            var response = rootObject.Items.FirstOrDefault();

            if (response != null)
            {
                return response.Snippet.Title + " (Views: " + response.Statistics.ViewCount.ToString("#,##0") + ")";
            }

            return "No result was found for video id " + id;
        }

        /// <summary> 
        /// The execute. 
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <typeparam name="T">
        /// The Type
        /// </typeparam>
        /// <returns> The <see cref="T"/>. </returns>
        private T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient() { BaseUrl = BaseUrl };
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("key", this.ApiKey);
            IRestResponse<T> restResponse = client.Execute<T>(request);
            return restResponse.Data;
        }
    }
}