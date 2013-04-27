using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Skypnet.Core;

namespace Skypnet.Modules.Mailer
{
    public class MailerModule : NinjectModule
    {
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
