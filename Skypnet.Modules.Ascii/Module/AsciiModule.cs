using Ninject.Modules;
using Skypnet.Core;

namespace Skypnet.Modules.Ascii.Module
{
    public class AsciiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISkypnetModule>()
                .To<AsciiSkypnetModule>()
                .WithPropertyValue("Name", "Ascii")
                .WithPropertyValue("Description", "Ascii art! Yaaaay ~")
                .WithPropertyValue("Instructions", "~ascii emote i.e '~ascii thumbsup'");
        }
    }
}
