using System;
using System.Collections.Generic;
using System.Threading;

namespace HEngine.Utility
{
    public static class RandomHelper
    {
        // TODO: Random.Shared도 유니티는 사용할 수 없다..
        private static readonly ThreadLocal<Random> _local = new(() => new Random());
        public static Random Shared => _local.Value!;
        
        public static T RandomElement<T>(this IList<T> list)
        {
            //var index = Random.Shared.Next(list.Count);
            var index = Shared.Next(list.Count);
            return list[index];
        }
    }
}

