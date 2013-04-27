using SKYPE4COMLib;

namespace Skypnet.Client
{
    /// <summary>
    /// The SkypeContainer is passed to all module instances
    /// so that the same Skype interface instance is accessed
    /// to allow modules to register their event handlers 
    /// </summary>
    public class SkypeContainer
    {
        /// <summary>
        /// The Skype Interface COM
        /// There is only ever one instance.
        /// </summary>
        public Skype Skype { get; set; }

        public SkypeContainer()
        {
            Skype = new Skype();
            Skype.Attach(Protocol: 8, Wait: false);
        }
    }
}