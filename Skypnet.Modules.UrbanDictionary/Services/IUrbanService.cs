// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUrbanService.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The UrbanService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.UrbanDictionary.Services
{
    /// <summary>
    /// The UrbanService interface.
    /// </summary>
    public interface IUrbanService
    {
        /// <summary>
        /// The search.
        /// </summary>
        /// <param name="urbanRequest">
        /// The urban request.
        /// </param>
        /// <returns>
        /// The <see cref="UrbanResponse"/>.
        /// </returns>
        UrbanResponse Search(UrbanRequest urbanRequest);
    }
}