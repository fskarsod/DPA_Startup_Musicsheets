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

        public ITrackBuilder SetName(string name)
        {
            _track.Name = name;
            return this;
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

        private BarBuilder _aggregateProviderBuilder;

        private double _currentLengthvalue;

        private BarBuilder AggregateProviderBuilder
        {
            get
            {
                if (_aggregateProviderBuilder != null)
                    return _aggregateProviderBuilder;
                _aggregateProviderBuilder = new BarBuilder();
                _aggregateProviderBuilder.SetTimeSignature(_timeSignature);
                _aggregateProviderBuilder.SetTempo(_tempo);
                _currentLengthvalue = 0d;
                return _aggregateProviderBuilder;
            }
            set { _aggregateProviderBuilder = value; }
        }

        private TimeSignature _timeSignature;

        public IAggregateBuilder<Track, BaseNote> Add(TimeSignature timeSignature, BaseNote element)
        {
            if (_timeSignature == null || !_timeSignature.Equals(timeSignature))
            {
                _timeSignature = timeSignature;
            }
            AddToAggregation(element);
            BuildBar();
            return this;
        }

        private Tempo _tempo;

        public IAggregateBuilder<Track, BaseNote> Add(TimeSignature timeSignature, BaseNote element, Tempo tempo)
        {
            _tempo = tempo;

            return Add(timeSignature, element);
        }

        private void AddToAggregation(BaseNote element)
        {
            AggregateProviderBuilder.AddComponent(element);
            _currentLengthvalue += element.LengthValue;
        }

        private bool IsBarFull()
        {
            return _timeSignature != null && _currentLengthvalue >= _timeSignature.TotalLengthValue;
        }

        private void BuildBar(bool @throw = false)
        {
            if (IsBarFull())
            {
                _track.MusicComponentProviders.Add(AggregateProviderBuilder.Build());
                AggregateProviderBuilder = null;
            }
            else if (@throw)
            {
                throw new InvalidOperationException("Unfinished bar is present in the track.");
            }
        }

        public Track Build()
        {
            if (_aggregateProviderBuilder != null)
            {
                BuildBar(true);
            }
            return _track;
        }
    }
}
