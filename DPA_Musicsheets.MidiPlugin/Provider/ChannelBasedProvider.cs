using System.Collections.Generic;
using DPA_Musicsheets.Core.Builder;
using DPA_Musicsheets.Core.Interface;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.MidiPlugin.Provider
{
    public class ChannelBasedProvider : IMusicComponentProvider
    {
        private readonly MidiEvent _startEvent;

        private readonly MidiEvent _stopEvent;

        private readonly int _absoluteTicks;

        private ChannelMessage StartMessage { get { return _startEvent.MidiMessage as ChannelMessage; } }

        private ChannelMessage StopMessage { get { return _stopEvent.MidiMessage as ChannelMessage; } }

        public ChannelBasedProvider(MidiEvent startEvent, MidiEvent stopEvent, int absoluteTicks)
        {
            _startEvent = startEvent;
            _stopEvent = stopEvent;
            _absoluteTicks = absoluteTicks;
        }

        public IEnumerable<IMusicComponent> GetMusicComponents()
        {
            var restDeltaTicks = _startEvent.AbsoluteTicks - _absoluteTicks;
            if (restDeltaTicks > 0) // rest before this channel event.
            {
                // todo: insert rest
                var restBuilder = new RestBuilder();
                restBuilder.SetDuration(1).HasDot(false); // todo: get correct length with potential dot
                yield return restBuilder.Build();
            }

            var eventDeltaTicks = _stopEvent.AbsoluteTicks - _startEvent.AbsoluteTicks; // tick count event duration
            var builder = new NoteBuilder();

            yield return builder.Build();
        }
    }
}
