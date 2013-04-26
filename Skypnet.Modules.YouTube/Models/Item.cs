namespace Skypnet.Modules.YouTube.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Kind { get; set; }
        public string Etag { get; set; }
        public Snippet Snippet { get; set; }
        public ContentDetails ContentDetails { get; set; }
        public Statistics Statistics { get; set; }
        public Status Status { get; set; }
    }
}