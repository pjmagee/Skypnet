using System;
using System.Threading;
using Ninject;
using Skypnet.Client;
using Skypnet.Core;
using Skypnet.Modules.About;
using Skypnet.Modules.Dice;
using Skypnet.Modules.Help;
using Skypnet.Modules.Weather;

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
                kernel.Bind<SkypeContainer>().ToSelf().InSingletonScope();


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
