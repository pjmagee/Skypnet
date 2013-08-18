// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleUrbanService.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The simple urban service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.UrbanDictionary.Services
{
    using System;
    using System.Net;
    using System.Text.RegularExpressions;
    using RestSharp.Contrib;

    /// <summary>
    /// The simple urban service.
    /// </summary>
    public class SimpleUrbanService : IUrbanService
    {
        /// <summary>The url.</summary>
        private const string Url = "http://www.urbandictionary.com/define.php?term=";

        /// <summary>The word pattern.</summary>
        private const string WordPattern = @"<td class='word'>\n(.*?)\n</td>";

        /// <summary>The definition pattern.</summary>
        private const string DefinitionPattern = @"<div class=""definition"">(.*?)</div>";

        /// <summary>The example pattern.</summary>
        private const string ExamplePattern = @"<div class=""example"">(.*?)</div>";

        /// <summary>The options.</summary>
        private const RegexOptions Options = RegexOptions.None;

        /// <summary>The definition regex.</summary>
        private readonly Regex definitionRegex = new Regex(DefinitionPattern, Options);

        /// <summary>The example regex.</summary>
        private readonly Regex exampleRegex = new Regex(ExamplePattern, Options);

        /// <summary>The word regex.</summary>
        private Regex wordRegex = new Regex(WordPattern, Options);

        /// <summary>The search to perform on Urban Dictionary.</summary>
        /// <param name="urbanRequest">The urban request.</param>
        /// <returns>The <see cref="UrbanResponse"/>.</returns>
        public UrbanResponse Search(UrbanRequest urbanRequest)
        {
            try
            {
                var html = new WebClient().DownloadString(Url + HttpUtility.UrlEncode(urbanRequest.Term));

                var definition = this.definitionRegex.Match(html);
                var example = this.exampleRegex.Match(html);
                var word = this.exampleRegex.Match(html);

                return new UrbanResponse
                {
                    Definition = HttpUtility.HtmlDecode(Regex.Replace(definition.Groups[1].Captures[0].Value, "<.*?>", string.Empty)),
                    Example = HttpUtility.HtmlDecode(Regex.Replace(example.Groups[1].Captures[0].Value, "<.*?>", string.Empty)),
                    Url = Url + urbanRequest.Term,
                    Word = Regex.Replace(word.Groups[1].Captures[0].Value, "<.*?>", string.Empty),
                };
            }
            catch (Exception)
            {
            }

            return null;
        }
    }
}