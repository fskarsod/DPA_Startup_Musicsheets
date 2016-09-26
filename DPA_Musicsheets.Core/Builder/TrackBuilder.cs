using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Builder.Interface;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Model;

namespace DPA_Musicsheets.Core.Builder
{
    public class TrackBuilder : ITrackBuilder
    {
        private readonly Track _track;

        public TrackBuilder()
        {
            _track = new Track();
        }

        public ITrackBuilder AddBar(Action<IBarBuilder> builderAction)
        {
            return AddMusicComponentProviderFromBuilder<BarBuilder>(new BarBuilder(), builderAction);
        }

        public ITrackBuilder AddRepetition(Action<IRepetitionBuilder> builderAction)
        {
            return AddMusicComponentProviderFromBuilder<RepetitionBuilder>(new RepetitionBuilder(), builderAction);
        }

        private ITrackBuilder AddMusicComponentProviderFromBuilder<TBuilder>(TBuilder builder, Action<TBuilder> builderAction)
            where TBuilder : IFluentBuilder<IMusicComponentProvider>
        {
            builderAction(builder);
            _track.MusicComponentProviders.Add(builder.Build());
            return this;
        }

        public Track Build()
        {
            return _track;
        }
    }
}
