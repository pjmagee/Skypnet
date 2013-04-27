using System;
using System.Linq;
using Ninject;
using RestSharp;
using Skypnet.Modules.YouTube.Models;

namespace Skypnet.Modules.YouTube
{
    public class YouTubeProvider : IYouTubeProvider
    {
        public string ApiKey { get; set; }

        private const string BaseUrl = "https://www.googleapis.com/";

        public string GetVideoInformation(string id)
        {
            var request = new RestRequest
                {
                    Method = Method.GET,
                    Resource = "youtube/v3/videos"
                };

            request.AddParameter("id", id);
            request.AddParameter("part", "snippet,contentDetails,statistics,status");

            var rootObject = Execute<RootObject>(request);

            var response = rootObject.Items.FirstOrDefault();

            if (response != null)
            {
                return response.Snippet.Title + " (Views: " + response.Statistics.ViewCount.ToString("#,##0") + ")";
            }

            return "No result was found for video id " + id;
        }

        private T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient() { BaseUrl = BaseUrl };
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("key", ApiKey);
            var restResponse = client.Execute<T>(request);
            return restResponse.Data;
        }
    }
}