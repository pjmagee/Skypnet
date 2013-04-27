using Ninject.Modules;
using Skypnet.Core;

namespace Skypnet.Modules.About.Module
{
    public class AboutModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISkypnetModule>()
                .To<AboutSkypnetModule>()
                .WithPropertyValue("Name", "About")
                .WithPropertyValue("Description", "Module which gives information about skypnet")
                .WithPropertyValue("Instructions", "!about");
        }
    }
}