// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YouTubeModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The you tube module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.YouTube.Module
{
    using Ninject.Modules;
    using Skypnet.Core;

    /// <summary>
    /// The you tube module.
    /// </summary>
    public class YouTubeModule : NinjectModule
    {
        /// <summary>
        /// The load.
        /// </summary>
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