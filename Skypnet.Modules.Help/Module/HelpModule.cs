using Ninject.Modules;
using Skypnet.Core;

namespace Skypnet.Modules.Help.Module
{
    public class HelpModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISkypnetModule>()
                .To<HelpSkypnetModule>()
                .WithPropertyValue("Name", "Help")
                .WithPropertyValue("Description", "A module that returns information about modules.")
                .WithPropertyValue("Instructions", "!help [module] i.e '!help About' or '!help Weather' or just !help to bring back a list of modules");
        }
    }
}