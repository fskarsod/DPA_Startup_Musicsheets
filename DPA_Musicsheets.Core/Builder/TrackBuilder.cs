using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Builder.Interface;

namespace DPA_Musicsheets.Core.Builder
{
    public class TrackBuilder<TTrack> : ITrackBuilder<TTrack>
    {
        private TTrack _track;

        public ITrackBuilder<TTrack> AddBar<TBar>(Action<IBarBuilder<TBar>> builderAction)
        {
            return AddMusicComponentProviderFromBuilder<IBarBuilder<TBar>, TBar>(builderAction, new BarBuilder<TBar>());
        }

        public ITrackBuilder<TTrack> AddRepetition<TRepetition>(Action<IRepetitionBuilder<TRepetition>> builderAction)
        {
            return AddMusicComponentProviderFromBuilder<IRepetitionBuilder<TRepetition>, TRepetition>(builderAction, new RepetitionBuilder<TRepetition>());
        }

        private ITrackBuilder<TTrack> AddMusicComponentProviderFromBuilder<TBuilder, TMusicComponentProvider>(Action<TBuilder> builderAction, TBuilder builder)
            where TBuilder : IFluentBuilder<TMusicComponentProvider>
        {
            // builderAction(builder);
            // _track.MusicComponentProviders.Add(builder.Build());
            // return this;
            throw new NotImplementedException();
        }

        public TTrack Build()
        {
            return _track;
        }
    }
}
