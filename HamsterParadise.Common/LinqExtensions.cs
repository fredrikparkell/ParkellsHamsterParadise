using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterParadise.Common
{
    public static class LinqExtensions
    {
        private static Random random = new Random();

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
    }
}
