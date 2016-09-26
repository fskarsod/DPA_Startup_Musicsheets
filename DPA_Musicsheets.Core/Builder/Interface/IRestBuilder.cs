using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Core.Builder.Interface
{
    public interface IRestBuilder
    {
        IRestBuilder SetDuration(int duration);

        IRestBuilder HasDot(bool hasDot = true);
    }
}
