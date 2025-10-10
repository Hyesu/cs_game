using System;
using System.Collections.Generic;

namespace HEngine.Extensions
{
    public static class RandomExtensions
    {
        public static T RandomElement<T>(this IList<T> list)
        {
            var index = Random.Shared.Next(list.Count);
            return list[index];
        }
    }
}

