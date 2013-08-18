// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchPrototypeModule.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   Defines the SearchPrototypeModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.Search
{
    using System;
    using Skypnet.Core;

    /// <summary>
    /// The search prototype module.
    /// </summary>
    public class SearchPrototypeModule : AbstractSkypnetModule
    {
        /// <summary>
        /// The register event handlers.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public override void RegisterEventHandlers()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        // TODO: Implement Bing search 
        // https://datamarket.azure.com/dataset/5BA839F1-12CE-4CCE-BF57-A49D98D29A44

        // TODO: Implement Google search
        // https://developers.google.com/custom-search/v1/using_rest
    }
}
