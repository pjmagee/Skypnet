using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;

namespace Skypnet.Client.Module
{
    public class SkypeModule : NinjectModule
    {
        public override void Load()
        {
            Bind<SkypeContainer>().ToSelf().InSingletonScope();
        }
    }
}
