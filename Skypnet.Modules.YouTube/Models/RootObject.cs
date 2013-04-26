using System.Collections.Generic;

namespace Skypnet.Modules.YouTube.Models
{
    public class RootObject
    {
        public string Kind { get; set; }
        public string Etag { get; set; }
        public List<Item> Items { get; set; }
    }
}