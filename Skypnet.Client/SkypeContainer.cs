using SKYPE4COMLib;

namespace Skypnet.Client
{
    public class SkypeContainer
    {
        /// <summary>
        /// The Skype Interface COM
        /// </summary>
        public Skype Skype { get; set; }

        public SkypeContainer()
        {
            Skype = new Skype();
            Skype.Attach(Protocol: 8, Wait: false);
        }
    }
}