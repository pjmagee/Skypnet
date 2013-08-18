// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BotStatusChangedEventArgs.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The bot status changed event args.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Core
{
    using System;

    /// <summary>
    /// The bot status changed event args.
    /// </summary>
    public class BotStatusChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BotStatusChangedEventArgs"/> class.
        /// </summary>
        /// <param name="oldStatus">
        /// The old status.
        /// </param>
        /// <param name="newStatus">
        /// The new status.
        /// </param>
        public BotStatusChangedEventArgs(BotStatus oldStatus, BotStatus newStatus)
        {
            this.OldStatus = oldStatus;
            this.NewStatus = newStatus;
        }

        /// <summary>
        /// Gets the old status.
        /// </summary>
        public BotStatus OldStatus { get; private set; }

        /// <summary>
        /// Gets the new status.
        /// </summary>
        public BotStatus NewStatus { get; private set; }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Old Status: {0}, New Status: {1}", this.OldStatus, this.NewStatus);
        }
    }
}