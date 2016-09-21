using System;
using System.Text;
using DPA_Musicsheets.Core.Model;
using DPA_Musicsheets.MidiPlugin.Util;
using Sanford.Multimedia.Midi;
using Track = DPA_Musicsheets.Core.Model.Track;

namespace DPA_Musicsheets.MidiPlugin.Grouping
{
    public class MetaGroup : MidiGroup
    {
        public MidiEvent MidiEvent { get; set; }

        public MetaMessage MetaMessage { get { return MidiEvent.MidiMessage as MetaMessage; } }

        // public override int PreviousEventAbsoluteTickCount { get { return MidiEvent.AbsoluteTicks; } }

        public MetaGroup(TrackMeta trackMeta, MidiEvent @event)
            : base(trackMeta)
        {
            if (@event == null)
                throw new ArgumentNullException("@event");

            MidiEvent = @event;
        }

        public override void CommitEvent(Track track)
        {
            var bytes = MetaMessage.GetBytes();
            switch (MetaMessage.MetaType)
            {
                case MetaType.Tempo:
                    // Bitshifting is nodig om het tempo in BPM te be
                    var tempo = (bytes[0] & 0xff) << 16 | (bytes[1] & 0xff) << 8 | (bytes[2] & 0xff);
                    var bpm = 60000000 / tempo;
                    TrackMeta.Tempo = bpm;
                    break;
                case MetaType.TimeSignature:                               //kwart = 1 / 0.25 = 4
                    TrackMeta.TimeSignature = new Fraction(bytes[0], Convert.ToUInt16(1 / Math.Pow(bytes[1], -2)));
                    break;
                //case MetaType.KeySignature:
                //case MetaType.ProprietaryEvent:
                //case MetaType.SmpteOffset:
                //// break;
                //case MetaType.TrackName:
                //default:
                //    break; // return MetaMessage.MetaType + ": " + Encoding.Default.GetString(MetaMessage.GetBytes());
            }
        }
    }
}
