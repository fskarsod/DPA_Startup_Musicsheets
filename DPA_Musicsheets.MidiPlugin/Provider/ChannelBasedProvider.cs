using System.Collections.Generic;
using DPA_Musicsheets.Core.Builder;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Model;
using DPA_Musicsheets.MidiPlugin.Util;
using Sanford.Multimedia.Midi;
using DPA_Musicsheets.Core.Builder.Interface;
using System.Linq;
using DPA_Musicsheets.MidiPlugin.Interface;

namespace DPA_Musicsheets.MidiPlugin.Provider
{
    public class ChannelBasedProvider : IBaseNoteProvider
    {
        private readonly TrackMeta _trackMeta;

        private readonly MidiEvent _startEvent;

        private readonly MidiEvent _stopEvent;

        private readonly int _absoluteTicks;

        private ChannelMessage StartMessage => _startEvent.MidiMessage as ChannelMessage;

        private ChannelMessage StopMessage => _stopEvent.MidiMessage as ChannelMessage;

        public ChannelBasedProvider(TrackMeta trackMeta, MidiEvent startEvent, MidiEvent stopEvent, int absoluteTicks)
        {
            _trackMeta = trackMeta;
            _startEvent = startEvent;
            _stopEvent = stopEvent;
            _absoluteTicks = absoluteTicks;
        }

        public IEnumerable<BaseNote> GetBaseNotes()
        {
            if (!IsValidProvider())
                yield break;
            var deltaTicks = _startEvent.AbsoluteTicks - _absoluteTicks; // delta ticks between previous provider and start event
            if (deltaTicks > 0) // rest before this channel event.
            {
                yield return BuildRest(deltaTicks);
            }
            yield return BuildNote();
        }

        private bool IsValidProvider()
        {
            return _startEvent != null && _stopEvent != null
                && HasValidChannelCommandScheme()
                && HasValidIntensityScheme();
        }

        private bool HasValidChannelCommandScheme()
        {
            return (StartMessage?.Command == ChannelCommand.NoteOn && StopMessage?.Command == ChannelCommand.NoteOn)
                || (StartMessage?.Command == ChannelCommand.NoteOn && StopMessage?.Command == ChannelCommand.NoteOff);
        }

        private bool HasValidIntensityScheme()
        {
            return StopMessage.Data2 == 0
                && StartMessage.Data2 > StopMessage.Data2;
        }

        private Note BuildNote()
        {
            var noteBuilder = new NoteBuilder();
            MidiNoteHelper.NoteTypeConverter(noteBuilder, StartMessage.Data1);
            var deltaTicks = _stopEvent.AbsoluteTicks - _startEvent.AbsoluteTicks; // delta ticks between start and stop event
            ConfigureLength(noteBuilder, deltaTicks);
            return noteBuilder.Build();
        }

        private Rest BuildRest(int deltaTicks)
        {
            var restBuilder = new RestBuilder();
            ConfigureLength(restBuilder, deltaTicks);
            return restBuilder.Build();
        }

        private void ConfigureLength<T>(IMusicComponentLengthBuilder<T> builder, double deltaTicks)
            where T : IMusicComponentLengthBuilder<T>
        {
            if (deltaTicks.Equals(0d))
            {
                MidiNoteHelper.NoteLengthConverter(builder);
            }
            else
            {
                var percentageOfSingleBeat = deltaTicks / _trackMeta.SequenceDivision;      // 384  / 384   == 1    | 288   / 384   == .75
                var expectedNoteInSingleBeat = 1d / _trackMeta.TimeSignature.Denominator;   // 1    / 4     == .25  | 1     / 4     == .25
                var usedNote = expectedNoteInSingleBeat * percentageOfSingleBeat;           // 1    * .25   == .25  | .75   * .25   == .1875

                MidiNoteHelper.NoteLengthConverter(builder, usedNote);                      // magic conversion from ticks to fraction.}
            }
        }
    }
}
