using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Model;

namespace DPA_Musicsheets.Core.Builder.Interface
{
    public interface ITrackBuilder : IAggregateBuilder<Track, BaseNote>
    {
        ITrackBuilder SetName(string name);

        ITrackBuilder AddBar(Action<IBarBuilder> builderAction);

        ITrackBuilder AddRepetition(Action<IRepetitionBuilder> builderAction);
    }
}
