using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Core.Builder.Interface
{
    public interface IRepetitionBuilder<out TBuildable>
        : IFluentBuilder<TBuildable>
    {
        IRepetitionBuilder<TBuildable> Repeat(int times);

        IRepetitionBuilder<TBuildable> AddBar<TBar>(Action<IBarBuilder<TBar>> builderAction);

        // todo: stuff with alternative endings;
    }
}
