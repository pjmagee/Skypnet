// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The enumerable extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Common
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The enumerable extensions.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// The do.
        /// </summary>
        /// <typeparam name="T">
        /// The type
        /// </typeparam>
        /// <param name="enumerable">
        /// The enumerable.
        /// </param>
        /// <param name="action">
        /// The action. T being the item, int being the index
        /// </param>
        public static void Do<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
            int i = 0;

            foreach (var item in enumerable)
            {
                action(item, i);
                i++;
            }
        }
    }
}