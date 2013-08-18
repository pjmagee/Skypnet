// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntegerExtensions.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The integer extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Common
{
    using System;

    /// <summary>
    /// The integer extensions.
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// Complete an action a number of times.
        /// Taken from StackOverflow
        /// http://stackoverflow.com/questions/177538/any-chances-to-imitate-times-ruby-method-in-c
        /// </summary>
        /// <param name="count">The number of times</param>
        /// <param name="action">Action with exposed iterator</param>
        public static void Times(this int count, Action<int> action)
        {
            for (int i = 0; i < count; i++)
            {
                action(i);
            }
        }

        /// <summary>
        /// The times.
        /// </summary>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        public static void Times(this int count, Action action)
        {
            for (int i = 0; i < count; i++)
            {
                action();
            }
        }
    }
}
