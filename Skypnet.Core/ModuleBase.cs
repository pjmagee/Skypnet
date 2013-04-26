using Ninject;

namespace Skypnet.Core
{
    public abstract class ModuleBase : IModule
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string Instructions { get; }
        public abstract void RegisterEventHandlers();

        /// <summary>
        /// Contains the list of loaded modules
        /// </summary>
        [Inject]
        public ModuleManager ModuleManager { get; set; }
    }
}