// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UrlShortenModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   Defines the UrlShortenModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.UrlShortener.Module
{
    using Ninject.Modules;
    using Skypnet.Core;
    using Skypnet.Modules.UrlShortener.Services;
    using Skypnet.Modules.UrlShortener.Services.GoogleShortener;
    using Skypnet.Modules.UrlShortener.Services.TinyUrl;

    /// <summary>
    /// The url shorten module.
    /// </summary>
    public class UrlShortenModule : NinjectModule
    {
        /// <summary>
        /// The load.
        /// </summary>
        public override void Load()
        {          
            Bind<ISkypnetModule>()
                .To<UrlShortenSkypnetModule>()
                .Named("Tinyurl")
                .WithPropertyValue("Name", "Tinyurl")
                .WithPropertyValue("Description", "Minifies a long url using the tinyurl service.")
                .WithPropertyValue("Instructions", "!minify tinyurl [url] i.e '!minify tinyurl http://example.com/a-really-long-url-you-want-to-minify'")
                .WithPropertyValue("Trigger", "tinyurl");

            Bind<ISkypnetModule>()
                .To<UrlShortenSkypnetModule>()
                .Named("Google")
                .WithPropertyValue("Name", "Google Shortener")
                .WithPropertyValue("Description", "Minifies a long url using the Google url shortener service.")
                .WithPropertyValue("Instructions", "!minify google [url] i.e '!minify google http://example.com/a-really-long-url-you-want-to-minify'")
                .WithPropertyValue("Trigger", string.Empty);

            // Well the tiny url provider should be injected 
            // into urlshortener named tinyurl
            Bind<IUrlShortenProvider>()
                .To<TinyUrlProvider>()
                .WhenParentNamed("Tinyurl")
                .WithPropertyValue("ApiKey", string.Empty);

            // Well the google url service should be injected
            // into urlshortener named google
            Bind<IUrlShortenProvider>()
                .To<GoogleUrlProvider>()
                .WhenParentNamed("Google")
                .WithPropertyValue("ApiKey", "AIzaSyC3diVUpQjg2niWpu84Be5hByOkg2-MsuU");
        }
    }
}