using System;
using System.Collections.Generic;
using System.Linq;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Model;
using DPA_Musicsheets.MidiPlugin.Interface;
using Sanford.Multimedia.Midi;
using DPA_Musicsheets.MidiPlugin.Util;

namespace DPA_Musicsheets.MidiPlugin.Provider
{
    public class MetaBasedProvider : IBaseNoteProvider
    {
        private readonly TrackMeta _trackMeta;

        private readonly MidiEvent _event;

        public MetaMessage EventMessage => _event.MidiMessage as MetaMessage;

        public MetaBasedProvider(TrackMeta trackMeta, MidiEvent @event)
        {
            _trackMeta = trackMeta;
            _event = @event;
        }

        public IEnumerable<BaseNote> GetBaseNotes()
        {
            RetrieveMetadata();
            return Enumerable.Empty<BaseNote>();
        }

        private void RetrieveMetadata()
        {
            var bytes = EventMessage.GetBytes();
            switch (EventMessage.MetaType)
            {
                //case MetaType.Tempo:
                //    // Bitshifting is nodig om het tempo in BPM te be
                //    var tempo = (bytes[0] & 0xff) << 16 | (bytes[1] & 0xff) << 8 | (bytes[2] & 0xff);
                //    var bpm = 60000000 / tempo;
                //    _trackMeta.Tempo = bpm;
                //    break;
                case MetaType.TimeSignature:                               //kwart = 1 / 0.25 = 4
                    _trackMeta.TimeSignature = new TimeSignature(bytes[0], Convert.ToUInt16(1 / Math.Pow(bytes[1], -2)));
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
