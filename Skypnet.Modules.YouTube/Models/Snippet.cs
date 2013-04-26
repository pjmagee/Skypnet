namespace Skypnet.Modules.YouTube.Models
{
    public class Snippet
    {
        public string PublishedAt { get; set; }
        public string ChannelId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Thumbnails Thumbnails { get; set; }
        public string CategoryId { get; set; }
    }
}