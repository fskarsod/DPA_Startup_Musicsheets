using System;
using System.Collections.Generic;
using DPA_Musicsheets.Core.Builder;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Model;
using DPA_Musicsheets.MidiPlugin.Provider;
using DPA_Musicsheets.MidiPlugin.Util;
using Sanford.Multimedia.Midi;

using SanfordTrack = Sanford.Multimedia.Midi.Track;

namespace DPA_Musicsheets.MidiPlugin.Plugin
{
    public class MidiPluginWriter : IPluginWriter<Sequence>
    {
        private static readonly IDictionary<MessageType, Func<int, Queue<MidiEvent>, IMusicComponentProvider>> EventLinker = new SortedDictionary<MessageType, Func<int, Queue<MidiEvent>, IMusicComponentProvider>>
        {
            { MessageType.Channel, (absoluteTicks, queue) => new ChannelBasedProvider(queue.Dequeue(), queue.Dequeue(), absoluteTicks) },
            { MessageType.Meta, (absoluteTicks, queue) => new MetaBasedProvider(queue.Dequeue(), absoluteTicks) }
        };

        public Sheet WriteSheet(Sequence source)
        {
            foreach (var sourceTrack in source)
            {
                var builder = new TrackBuilder();

            }
        }

        // Only notes and rests, bars during enumeration.
        private static IEnumerable<IMusicComponentProvider> GetProviders(SanfordTrack sourceTrack)
        {
            var queue = new FixedSizeQueue<MidiEvent>(2);
            var absoluteTicks = 0;
            foreach (var @event in sourceTrack.Iterator())
            {
                queue.Enqueue(@event);
                if (EventLinker.ContainsKey(@event.MidiMessage.MessageType))
                {
                    var result = EventLinker[@event.MidiMessage.MessageType](absoluteTicks, queue);
                    if (result != null)
                        yield return result;
                }
                absoluteTicks = @event.AbsoluteTicks;
            }
        }
    }
}
