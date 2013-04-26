using System;

namespace Skypnet.Core
{
    public class BotStatusChangedEventArgs : EventArgs
    {
        public BotStatus OldStatus { get; private set; }
        public BotStatus NewStatus { get; private set; }

        public BotStatusChangedEventArgs(BotStatus oldStatus, BotStatus newStatus)
        {
            OldStatus = oldStatus;
            NewStatus = newStatus;
        }

        public override string ToString()
        {
            return string.Format("Old Status: {0}, New Status: {1}", OldStatus, NewStatus);
        }
    }
}