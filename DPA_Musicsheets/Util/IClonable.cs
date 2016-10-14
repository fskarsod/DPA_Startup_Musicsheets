using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Util
{
    public interface IClonable<out T>
    {
        T Clone();
    }
}
