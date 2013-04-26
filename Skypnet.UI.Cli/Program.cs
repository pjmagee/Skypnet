using System;
using System.Threading;
using Ninject;
using Skypnet.Client;
using Skypnet.Core;
using Skypnet.Modules.About;
using Skypnet.Modules.Dice;
using Skypnet.Modules.Help;
using Skypnet.Modules.UrlShortener;
using Skypnet.Modules.UrlShortener.GoogleShortener.Models;
using Skypnet.Modules.Weather;
using Skypnet.Modules.Weather.Wunderground;
using Skypnet.Modules.YouTube;

namespace Skypnet.UI.Cli
{
    internal class ConsoleRunner
    {
        public static void Main()
        {
            try
            {
                IKernel kernel = new StandardKernel();

                kernel.Bind<IModule>().To<AboutModule>();
                kernel.Bind<IModule>().To<DiceModule>();
                kernel.Bind<IModule>().To<HelpModule>();
                kernel.Bind<IModule>().To<WeatherModule>();
                kernel.Bind<IModule>().To<YouTubeModule>();
                kernel.Bind<IModule>().To<UrlShortenerModule>();

                kernel.Bind<SkypeContainer>().ToSelf().InSingletonScope(); // One instance of Skype

                // TODO: Read api key from App config

                kernel.Bind<IYouTubeProvider>().To<YouTubeProvider>()
                    .WithPropertyValue("ApiKey", "AIzaSyC3diVUpQjg2niWpu84Be5hByOkg2-MsuU");

                kernel.Bind<IUrlShortenerProvider>().To<UrlShortenerProvider>()
                      .WithPropertyValue("ApiKey", "AIzaSyC3diVUpQjg2niWpu84Be5hByOkg2-MsuU");

                kernel.Bind<IWeatherProvider>().To<WundergroundWeatherProvider>()
                    .WithPropertyValue("ApiKey", "3ffac173009f680a");
                
                using (var shutdownEvent = new ManualResetEventSlim(false))
                {
                    var bot = kernel.Get<SkypnetBot>();

                    bot.BotStatusChanged += ((sender, args) =>
                    {
                        if (args.NewStatus == BotStatus.Stopped)
                        {
                            shutdownEvent.Set();
                        }
                    });

                    Console.CancelKeyPress += ((sender, args) =>
                    {
                        args.Cancel = true;
                        bot.Stop();
                        shutdownEvent.Set();
                    });

                    bot.Start();
                    shutdownEvent.Wait();
                    kernel.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
            }
        }

    }
}
