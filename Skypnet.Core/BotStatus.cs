// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BotStatus.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The bot status.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Core
{
    /// <summary>
    /// The bot status.
    /// </summary>
    public enum BotStatus
    {
        /// <summary>
        /// The bot is starting. This includes loading modules and any other initialization logic.
        /// </summary>
        Starting,

        /// <summary>
        /// The bot has started and is now fully functional.
        /// </summary>
        Started,

        /// <summary>
        /// The bot is stopping.
        /// </summary>
        Stopping,

        /// <summary>
        /// The bot has finished stopping.
        /// </summary>
        Stopped
    }
}
