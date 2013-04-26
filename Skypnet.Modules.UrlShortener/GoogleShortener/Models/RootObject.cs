using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skypnet.Modules.UrlShortener.GoogleShortener.Models
{
    /*
     *  https://developers.google.com/url-shortener/v1/getting_started#shorten
     * 
     * {
     *   "kind": "urlshortener#url",
     *   "id": "http://goo.gl/fbsS",
     *   "longUrl": "http://www.google.com/"
     * }
     * 
     */

    public class RootObject
    {
        public string Kind { get; set; }
        public string Id { get; set; }
        public string LongUrl { get; set; }
    }
}
