// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UrbanResponse.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The urban response.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.UrbanDictionary.Services
{
    /// <summary>
    /// The urban response.
    /// </summary>
    public class UrbanResponse
    {
        /// <summary>
        /// Gets or sets the word.
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        public int TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the definition.
        /// </summary>
        public string Definition { get; set; }

        /// <summary>
        /// Gets or sets the example.
        /// </summary>
        public string Example { get; set; }
    }
}