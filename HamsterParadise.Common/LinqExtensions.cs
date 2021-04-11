using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterParadise.Common
{
    /// <summary>
    /// Self-made custom Linq-extension-class that contains a method "Shuffle"
    /// which shuffles a collection and returns a IEnumerable<T> with shuffled values
    /// </summary>
    public static class LinqExtensions
    {
        #region Static Property
        private static Random random = new Random();
        #endregion

        #region Extension-Method
        /// <summary>
        /// A custom-made Linq extension made for shuffling/randomize the
        /// collection. In this project it is used to make sure that the
        /// hamsters that first go into the Exercise Area every morning is randomized.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns>An IEnumerable<T> with shuffled values.</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> data)
        {
            if (data.Count() == 0)
            {
                return null;
            }

            var newData = data.ToList();

            int newDataCount = newData.Count;
            while (newDataCount > 1)
            {
                newDataCount--;
                int randomValue = random.Next(newDataCount + 1);
                T value = newData[randomValue];
                newData[randomValue] = newData[newDataCount];
                newData[newDataCount] = value;
            }

            return (IEnumerable<T>)newData;
        }
        #endregion
    }
}
