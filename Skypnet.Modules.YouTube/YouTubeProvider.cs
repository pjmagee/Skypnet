using System;
using System.Linq;
using System.Text.RegularExpressions;
using RestSharp;
using Skypnet.Modules.YouTube.Models;

namespace Skypnet.Modules.YouTube
{
    public class YouTubeProvider : IYouTubeProvider
    {
        public string ApiKey { get; set; }

        private const string BaseUrl = "https://www.googleapis.com/";

        

        private T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient() { BaseUrl = BaseUrl };
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("key", ApiKey, ParameterType.GetOrPost);

            var restResponse = client.Execute<T>(request);
            return restResponse.Data;
        }

        public string GetVideoInformation(string id)
        {
            RestRequest request = new RestRequest()
                {
                    Method = Method.GET,
                    Resource = "youtube/v3/videos"
                };

            request.AddParameter("id", id);
            request.AddParameter("part", "snippet,contentDetails,statistics,status");

            RootObject rootObject = Execute<RootObject>(request);

            var response = rootObject.Items.FirstOrDefault();

            if (response != null)
            {
                return response.Snippet.Title + " [Views: " + response.Statistics.ViewCount.ToString("#,##0") + ")";
            }

            return "No result was found for video id" + id;
        }
    }
}