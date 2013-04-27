using System;
using System.Threading;
using Ninject;
using Ninject.Modules;
using Skypnet.Client.Module;
using Skypnet.Core;
using Skypnet.Modules.About.Module;
using Skypnet.Modules.Ascii.Module;
using Skypnet.Modules.Dice.Module;
using Skypnet.Modules.Help.Module;
using Skypnet.Modules.UrlShortener.Module;
using Skypnet.Modules.Weather.Module;
using Skypnet.Modules.YouTube.Module;

namespace Skypnet.UI.Cli
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                IKernel kernel = new StandardKernel();

                var modules = new INinjectModule[]
                    {
                        new SkypeModule(),
                        new AboutModule(),
                        new YouTubeModule(),
                        new AsciiModule(),
                        new DiceModule(),
                        new HelpModule(),
                        new UrlShortenerModule(),
                        new WeatherModule(),
                    };

                kernel.Load(modules);
                
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
