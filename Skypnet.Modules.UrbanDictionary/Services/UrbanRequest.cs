// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UrbanRequest.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The urban request.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.UrbanDictionary.Services
{
    /// <summary>
    /// The urban request.
    /// </summary>
    public class UrbanRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UrbanRequest"/> class.
        /// </summary>
        /// <param name="term">
        /// The term.
        /// </param>
        public UrbanRequest(string term)
        {
            this.Term = term;
        }

        /// <summary>
        /// Gets or sets the term.
        /// </summary>
        public string Term { get; set; }
    }
}