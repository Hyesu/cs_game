using System.Collections.Generic;

namespace HEngine.Extensions
{
    public static class EnumerableExtensions
    {
        public static void AddRange<T>(this HashSet<T> origin, IEnumerable<T> input)
        {
            foreach (var element in input)
            {
                origin.Add(element);
            }
        }
    }    
}