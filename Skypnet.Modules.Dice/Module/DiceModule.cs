using Ninject.Modules;
using Skypnet.Core;

namespace Skypnet.Modules.Dice.Module
{
    public class DiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISkypnetModule>()
                .To<DiceSkypnetModule>()
                .WithPropertyValue("Name", "Dice")
                .WithPropertyValue("Description", "Rolls a 6 sided die. Fun times!")
                .WithPropertyValue("Instructions", "!roll [optional number of times]");
        }
    }
}