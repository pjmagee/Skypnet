using Ninject.Modules;
using Skypnet.Core;
using Skypnet.Modules.UrlShortener.GoogleShortener;
using Skypnet.Modules.UrlShortener.Services;
using Skypnet.Modules.UrlShortener.Services.GoogleShortener;
using Skypnet.Modules.UrlShortener.Services.TinyUrl;

namespace Skypnet.Modules.UrlShortener.Module
{
    public class UrlShortenerModule : NinjectModule
    {
        public override void Load()
        {          
            Bind<ISkypnetModule>()
                .To<UrlShortenerSkypnetModule>()
                .Named("Tinyurl")
                .WithPropertyValue("Name", "Tinyurl")
                .WithPropertyValue("Description", "Minifies a long url using the tinyurl service.")
                .WithPropertyValue("Instructions", "!minify tinyurl [url] i.e '!minify tinyurl http://example.com/a-really-long-url-you-want-to-minify'")
                .WithPropertyValue("Trigger", "tinyurl");

            Bind<ISkypnetModule>()
                .To<UrlShortenerSkypnetModule>()
                .Named("Google")
                .WithPropertyValue("Name", "Google Shortener")
                .WithPropertyValue("Description", "Minifies a long url using the Google url shortener service.")
                .WithPropertyValue("Instructions", "!minify google [url] i.e '!minify google http://example.com/a-really-long-url-you-want-to-minify'")
                .WithPropertyValue("Trigger", "");

            // Well the tiny url provider should be injected 
            // into urlshortener named tinyurl
            Bind<IUrlShortenerProvider>()
                .To<TinyUrlProvider>()
                .WhenParentNamed("Tinyurl")
                .WithPropertyValue("ApiKey", "");

            // Well the google url service should be injected
            // into urlshortener named google
            Bind<IUrlShortenerProvider>()
                .To<GoogleUrlProvider>()
                .WhenParentNamed("Google")
                .WithPropertyValue("ApiKey", "AIzaSyC3diVUpQjg2niWpu84Be5hByOkg2-MsuU");
        }
    }
}