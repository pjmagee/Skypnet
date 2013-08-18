// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MailerModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The mailer module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.Mailer
{
    using Ninject.Modules;
    using Skypnet.Core;

    /// <summary>
    /// The mailer module.
    /// </summary>
    public class MailerModule : NinjectModule
    {
        /// <summary>
        /// The load.
        /// </summary>
        public override void Load()
        {
            Bind<ISkypnetModule>()
                .To<MailerSkypnetModule>()
                .WithPropertyValue("Name", "Mailer")
                .WithPropertyValue("Description", "Allows you to send mail to a specified email address.")
                .WithPropertyValue("Instructions", "!mail [address] [message] i.e '!mail patrick.mageee@live.co.uk They keys are under the door mat!");
        }
    }
}
