using System;
using Ninject.Modules;
using Skypnet.Core;

namespace Skypnet.Modules.YouTube.Module
{
    public class YouTubeModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISkypnetModule>()
                .To<YouTubeSkypnetModule>()
                .Named("Youtube")
                .WithPropertyValue("Name", "Youtube")
                .WithPropertyValue("Description", "Provides information about a youtube link.")
                .WithPropertyValue("Instructions", "Paste a link to a youtube video or !yt [video id] i.e '!yt 130491'");

            Bind<IYouTubeProvider>()
                .To<YouTubeProvider>()
                .WhenParentNamed("Youtube")
                .WithPropertyValue("ApiKey", "AIzaSyC3diVUpQjg2niWpu84Be5hByOkg2-MsuU");
        }
    }
}