namespace Skypnet.Modules.YouTube
{
    public interface IYouTubeProvider
    {
        string ApiKey { get; set; }
        string GetVideoInformation(string id);
    }
}