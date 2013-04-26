namespace Skypnet.Modules.UrlShortener
{
    public interface IUrlShortenerProvider
    {
        string ApiKey { get; set; }
        string Shorten(string url);
    }
}