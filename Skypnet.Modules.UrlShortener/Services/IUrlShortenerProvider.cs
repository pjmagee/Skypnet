namespace Skypnet.Modules.UrlShortener.Services
{
    public interface IUrlShortenerProvider
    {
        string ApiKey { get; set; }
        string Shorten(string url);
    }
}