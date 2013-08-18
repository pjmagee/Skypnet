// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The main console program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.UI.Cli
{
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
    using Skypnet.Modules.UrbanDictionary.Module;
    using Skypnet.Modules.UrlShortener.Module;
    using Skypnet.Modules.Weather.Module;
    using Skypnet.Modules.YouTube.Module;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point. 
        /// Composition root.
        /// We setup the Kernel here, prior to anything else.
        /// </summary>
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
                        new ASCIIModule(),
                        new DiceModule(),
                        new HelpModule(),
                        new UrlShortenModule(),
                        new WeatherModule(),
                        new UrbanModule()
                    };

                kernel.Load(modules);
                
                using (var shutdownEvent = new ManualResetEventSlim(false))
                {
                    var bot = kernel.Get<SkypnetBot>();

                    // Inline event listener.
                    bot.BotStatusChanged += (sender, args) =>
                    {
                        if (args.NewStatus == BotStatus.Stopped)
                        {
                            shutdownEvent.Set();
                        }
                    };

                    Console.CancelKeyPress += (sender, args) =>
                    {
                        args.Cancel = true;
                        bot.Stop();
                        shutdownEvent.Set();
                    };

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
