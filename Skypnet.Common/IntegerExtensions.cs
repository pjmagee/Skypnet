using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skypnet.Common
{
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

        public static void Times(this int count, Action action)
        {
            for (int i = 0; i < count; i++)
            {
                action();
            }
        }
    }
}
