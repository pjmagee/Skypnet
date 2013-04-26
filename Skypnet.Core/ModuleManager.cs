using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;

namespace Skypnet.Core
{
    public class ModuleManager
    {
        [Inject]
        public IEnumerable<IModule> Modules { get; set; } 

        public void RegisterModules()
        {
            Modules.ToList().ForEach(x => x.RegisterEventHandlers());
        }
    }
}