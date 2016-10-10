using System;
using System.Collections.Generic;
using System.Linq;
using DPA_Musicsheets.Core.Builder;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Model;
using DPA_Musicsheets.MidiPlugin.Interface;
using DPA_Musicsheets.MidiPlugin.Provider;
using DPA_Musicsheets.MidiPlugin.Util;
using Sanford.Multimedia.Midi;

using SanfordTrack = Sanford.Multimedia.Midi.Track;
using Track = DPA_Musicsheets.Core.Model.Track;

namespace DPA_Musicsheets.MidiPlugin.Plugin
{
    public class MidiPluginWriter : IPluginWriter<Sequence>
    {
        private static readonly IDictionary<MessageType, Func<BosonContainer<MidiEvent>, TrackMeta, IBaseNoteProvider>> EventLinker = new SortedDictionary<MessageType, Func<BosonContainer<MidiEvent>, TrackMeta, IBaseNoteProvider>>
        {
            { MessageType.Channel, (container, trackMeta) => new ChannelBasedProvider(trackMeta, container[1], container[2], container[0] != default(MidiEvent) ? container[0].AbsoluteTicks : 0) },
            { MessageType.Meta, (container, trackMeta) => new MetaBasedProvider(trackMeta, container[2]) }
        };

        private readonly TrackMeta _trackMeta;

        public MidiPluginWriter()
        {
            _trackMeta = new TrackMeta();
        }

        public Sheet WriteSheet(Sequence source)
        {
            var tracks = new List<Track>();
            _trackMeta.SequenceDivision = source.Division;
            foreach (var sourceTrack in source)
            {
                var builder = new TrackBuilder();
                foreach (var baseNote in GetBaseNotes(sourceTrack))
                {
                    builder.Add(_trackMeta.TimeSignature, baseNote);
                }
                tracks.Add(builder.Build());
            }
            return new Sheet { Tracks = tracks };
        }

        private IEnumerable<BaseNote> GetBaseNotes(SanfordTrack sourceTrack)
        {
            return GetProviders(sourceTrack).SelectMany(provider => provider.GetBaseNotes());
        }

        // Only notes and rests, bars during enumeration.
        private IEnumerable<IBaseNoteProvider> GetProviders(SanfordTrack sourceTrack)
        {
            var container = new BosonContainer<MidiEvent>(3);
            foreach (var @event in sourceTrack.Iterator())
            {
                container.Queue(@event);
                if (EventLinker.ContainsKey(@event.MidiMessage.MessageType))
                {
                    var result = EventLinker[@event.MidiMessage.MessageType](container, _trackMeta);
                    if (result != null)
                        yield return result;
                }
            }
        }
    }
}
