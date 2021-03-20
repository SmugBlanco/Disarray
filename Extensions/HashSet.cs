using System;
using System.Collections.Generic;

namespace Disarray.Extensions
{
    public static class HashSetExtensions
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return new HashSet<T>(source);
        }
    }
}