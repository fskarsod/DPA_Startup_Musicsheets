using System;
using DPA_Musicsheets.Core.Model;
using DPA_Musicsheets.MidiPlugin.Util;
using Sanford.Multimedia.Midi;

using TrackModel = DPA_Musicsheets.Core.Model.Track;
using NoteModel = DPA_Musicsheets.Core.Model.Note;
using RestModel = DPA_Musicsheets.Core.Model.Rest;

namespace DPA_Musicsheets.MidiPlugin.Grouping
{
    public class ChannelGroup : MidiGroup
    {
        private int _absoluteTickCount;

        public MidiEvent StartEvent { get; set; }

        public MidiEvent StopEvent { get; set; }

        public ChannelMessage StartChannelMessage { get { return StartEvent.MidiMessage as ChannelMessage; } }

        public ChannelMessage StopChannelMessage { get { return StopEvent.MidiMessage as ChannelMessage; } }

        public ChannelGroup(TrackMeta trackMeta, MidiEvent start, MidiEvent stop, int absoluteTickCount)
            : base(trackMeta)
        {
            if (start == null)
                throw new ArgumentNullException("start");

            StartEvent = start;
            StopEvent = stop;
            _absoluteTickCount = absoluteTickCount;
        }

        /*
         * Need Sequence.Division
         * Need Time Signature
         * 
         * Sequence.Division equals ticks of 1 single beat. (?)
         * With time signature, you can calculate how much.
         * 
         * Time Signature -> 2/8 -> 2 beats in a bar, 1/8th note is used (for one beat) -> 2x 1/8th note
         * 
         * Numeral                              ==  Ticks
         * TrackMeta.TimeSignature.Denominator  ==  Sequence.Division (?)
         * ???                                  ==  deltaTicks
         */
        public Fraction GetLength(double deltaTicks)
        {
            if (deltaTicks.Equals(0d))
                return MidiNoteHelper.NoteLengthConverter();

            var percentageOfSingleBeat = deltaTicks / TrackMeta.SequenceDivision;   // 384  / 384   == 1    | 288   / 384   == .75
            var expectedNoteInSingleBeat = 1d / TrackMeta.TimeSignature.Denominator;// 1    / 4     == .25  | 1     / 4     == .25
            var usedNote = expectedNoteInSingleBeat * percentageOfSingleBeat;       // 1    * .25   == .25  | .75   * .25   == .1875

            return MidiNoteHelper.NoteLengthConverter(usedNote);                    // magic conversion from ticks to fraction.
        }

        public NoteModel GetNote()
        {
            var delta = 0;
            if (StopEvent != null)
            {
                delta = StopEvent.AbsoluteTicks - StartEvent.AbsoluteTicks;
            }
            var note = MidiNoteHelper.NoteTypeConverter(StartChannelMessage.Data1);
            note.Length = GetLength(delta);
            note.TimeSignature = TrackMeta.TimeSignature;

            return note;
        }

        public RestModel GetRest()
        {
            var delta = StartEvent.AbsoluteTicks - _absoluteTickCount;
            return new RestModel
            {
                Length = GetLength(delta),
                TimeSignature = TrackMeta.TimeSignature
            };
        }

        public override void CommitEvent(TrackModel track)
        {
            if (StartEvent.AbsoluteTicks > _absoluteTickCount)
            {
                // todo: add rest;
                track.MusicFragments.Add(GetRest());
            }
            // todo: add note;
            track.MusicFragments.Add(GetNote());
        }
    }
}
