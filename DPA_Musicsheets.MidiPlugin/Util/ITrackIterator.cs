using DPA_Musicsheets.MidiPlugin.Grouping;
using System.Collections.Generic;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.MidiPlugin.Util
{
    public interface ITrackIterator
    {
        IEnumerable<MidiGroup> FromTrack(Track track, int sequenceDivision);
    }
}
