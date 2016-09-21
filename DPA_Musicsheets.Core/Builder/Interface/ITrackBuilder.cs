using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Core.Builder.Interface
{
    public interface ITrackBuilder<out TBuildable>
        : IFluentBuilder<TBuildable>
    {
        ITrackBuilder<TBuildable> AddBar<TBar>(Action<IBarBuilder<TBar>> builderAction);

        ITrackBuilder<TBuildable> AddRepetition<TRepetition>(Action<IRepetitionBuilder<TRepetition>> builderAction);
    }
}
