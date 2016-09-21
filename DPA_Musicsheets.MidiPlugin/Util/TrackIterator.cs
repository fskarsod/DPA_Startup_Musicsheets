using System;
using System.Collections.Generic;
using DPA_Musicsheets.MidiPlugin.Grouping;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.MidiPlugin.Util
{
    public class TrackIterator : ITrackIterator
    {
        private Track _track;
        private readonly TrackMeta _trackMeta;

        public TrackIterator()
        {
            _trackMeta = new TrackMeta();
        }

        public IEnumerable<MidiGroup> FromTrack(Track track, int sequenceDivision)
        {
            if (track == null)
                throw new ArgumentNullException("track");

            _track = track;
            _trackMeta.SequenceDivision = sequenceDivision;
            return Iterator();
        }

        private IEnumerable<MidiGroup> Iterator()
        {
            var step = 0;
            var tickCount = 0;
            MidiEvent storedEvent = null;
            foreach (var midiEvent in _track.Iterator())
            {
                switch (midiEvent.MidiMessage.MessageType)
                {
                    case MessageType.Channel: // ChannelMessages zijn de inhoudelijke messages.
                        var channel = midiEvent.MidiMessage as ChannelMessage;
                        if (channel != null && channel.Command != ChannelCommand.NoteOn) { break; }

                        if (step % 2 == 0 && storedEvent != null)
                        {
                            yield return new ChannelGroup(_trackMeta, storedEvent, midiEvent, tickCount);
                            tickCount = midiEvent.AbsoluteTicks;
                        }
                        else
                        {
                            storedEvent = midiEvent;
                        }
                        break;
                    case MessageType.Meta: // Meta zegt iets over de track zelf.
                        yield return new MetaGroup(_trackMeta, midiEvent);
                        break;
                    default: // 
                        yield return new MiscGroup(_trackMeta, midiEvent);
                        break;
                }
                step++;
            }
        }
    }
}
