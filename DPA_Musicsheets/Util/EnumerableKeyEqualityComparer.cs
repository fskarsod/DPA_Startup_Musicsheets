using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace DPA_Musicsheets.Util
{
    public class EnumerableKeyEqualityComparer : IEqualityComparer<IEnumerable<Key>>
    {
        /// <returns>
        ///     True when both Enumerations of Key have the same length,
        ///     contain the same keys, and keys are present in the exact same order.
        /// </returns>
        public bool Equals(IEnumerable<Key> x, IEnumerable<Key> y)
        {
            // Dispose of enumerators after use
            using (var xEnumerator = x.GetEnumerator())
            using (var yEnumerator = y.GetEnumerator())
            {
                while (xEnumerator.MoveNext() && yEnumerator.MoveNext())
                {
                    if (xEnumerator.Current != yEnumerator.Current)
                        return false;
                }
                return !xEnumerator.MoveNext()
                       && !yEnumerator.MoveNext();
            }
        }

        public int GetHashCode(IEnumerable<Key> obj)
        {
            return obj.Select(key => key.GetHashCode()).Aggregate(0, (current, hash) => current ^ hash);
        }
    }
}
