using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Core.Builder.Interface
{
    public interface INoteBuilder<out TBuildable>
        : IFluentBuilder<TBuildable>
    {
        INoteBuilder<TBuildable> SetDuration(int duration);

        INoteBuilder<TBuildable> HasDot(bool hasDot = true);

        INoteBuilder<TBuildable> SetPitch(int pitch); // todo: to enum

        INoteBuilder<TBuildable> SetAccidental(int accidental); // todo: to enum
    }
}
