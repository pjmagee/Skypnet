using System.Net.Http;

namespace Skypnet.Modules.UrlShortener.Services.TinyUrl
{
    public class TinyUrlProvider : IUrlShortenerProvider
    {
        public string ApiKey { get; set; }

        public string Shorten(string url)
        {
            var httpClient = new HttpClient();
            var result = httpClient.GetAsync("http://tinyurl.com/api-create.php?url=" + url).Result;
            return result.Content.ReadAsStringAsync().Result;
        }

    }
}
