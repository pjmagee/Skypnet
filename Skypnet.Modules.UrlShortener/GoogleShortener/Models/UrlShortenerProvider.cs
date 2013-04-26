using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace Skypnet.Modules.UrlShortener.GoogleShortener.Models
{
    public class UrlShortenerProvider : IUrlShortenerProvider
    {
        public string ApiKey { get; set; }

        private const string BaseUrl = "https://www.googleapis.com/";

        private T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient(BaseUrl + "/urlshortener/v1/url?key=" + ApiKey);                      
            var restResponse = client.Execute<T>(request);
            return restResponse.Data;
        }

        /// <summary>
        /// https://developers.google.com/url-shortener/v1/getting_started#shorten
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string Shorten(string url)
        {
            RestRequest request = new RestRequest()
                {
                    Method = Method.POST,
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new JsonSerializer(),
                };

            request.AddBody(new { longUrl = url });
            var rootObject = Execute<RootObject>(request);
            return rootObject.Id;
        }
    }
}