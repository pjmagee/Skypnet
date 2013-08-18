// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Status.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The status.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.YouTube.Models
{
    /// <summary>
    /// The status.
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Gets or sets the upload status.
        /// </summary>
        public string UploadStatus { get; set; }

        /// <summary>
        /// Gets or sets the privacy status.
        /// </summary>
        public string PrivacyStatus { get; set; }
    }
}