using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Core.Util
{
    public static class MathClass
    {
        public static bool IsPowerOfTwo(long x)
        {
            return (x != 0) && ((x & (x - 1)) == 0);
        }
    }
}
