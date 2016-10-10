using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Util
{
    public interface IRestorable<in T>
        where T : IRestorable<T>
    {
        bool Restore(T restorable);
    }
}
