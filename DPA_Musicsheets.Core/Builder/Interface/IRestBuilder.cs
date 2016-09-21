using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Core.Builder.Interface
{
    public interface IRestBuilder<out TBuildable>
        : IFluentBuilder<TBuildable>
    {
        IRestBuilder<TBuildable> SetDuration(int duration);

        IRestBuilder<TBuildable> HasDot(bool hasDot = true);
    }
}
