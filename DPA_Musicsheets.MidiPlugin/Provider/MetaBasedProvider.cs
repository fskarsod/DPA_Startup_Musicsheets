using System;
using System.Collections.Generic;
using DPA_Musicsheets.Core.Interface;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.MidiPlugin.Provider
{
    public class MetaBasedProvider : IMusicComponentProvider
    {
        private readonly MidiEvent _event;

        private readonly int _absoluteTicks;

        public MetaMessage EventMessage { get { return _event.MidiMessage as MetaMessage; } }

        public MetaBasedProvider(MidiEvent @event, int absoluteTicks)
        {
            _event = @event;
            _absoluteTicks = absoluteTicks;
        }

        public IEnumerable<IMusicComponent> GetMusicComponents()
        {
            throw new NotImplementedException();
        }
    }
}
