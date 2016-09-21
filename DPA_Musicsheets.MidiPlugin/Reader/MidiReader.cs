using System.Linq;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.MidiPlugin.Util;
using Sanford.Multimedia.Midi;

using SheetModel = DPA_Musicsheets.Core.Model.Sheet;
using TrackModel = DPA_Musicsheets.Core.Model.Track;

namespace DPA_Musicsheets.MidiPlugin.Reader
{
    public class MidiReader : ISheetReader<Sequence>
    {
        private readonly ITrackIterator _trackIterator;

        public MidiReader(ITrackIterator trackIterator)
        {
            _trackIterator = trackIterator;
        }

        public MidiReader()
            : this(new TrackIterator())
        { }

        public SheetModel ReadSheet(Sequence source)
        {
            var sheet = new SheetModel();
            foreach (var track in source) // De sequence heeft tracks. Deze zijn per index benaderbaar.
            {
                var trackModel = new TrackModel();
                foreach (var midiGroup in _trackIterator.FromTrack(track, source.Division))
                {
                    midiGroup.CommitEvent(trackModel);

                    // midiGroup.CommitEvent();

                    //if (midiEventSet.StartEvent == null || midiEventSet.StopEvent == null) { continue; }

                    //var startChannelMessage = midiEventSet.StartEvent.MidiMessage as ChannelMessage;
                    //var stopChannelMessage = midiEventSet.StopEvent.MidiMessage as ChannelMessage;

                    //if (startChannelMessage == null || stopChannelMessage == null) { continue; }

                    // if StartEvent.Absolute is greater than absolute, then insert a rest

                    //var note = MidiNoteHelper.NoteTypeConverter(startChannelMessage.Data1);
                    //note.Length = new Fraction { Numerator = 1, Denominator = 1 };
                    //note.TimeSignature = new Fraction { Numerator = 4, Denominator = 4 };

                    //mTrack.MusicFragments.Add(note);
                }
                if (trackModel.MusicFragments.Any()) // alternatively: mTrack.MusicFragments.Count > 0
                {
                    sheet.Tracks.Add(trackModel);
                }
            }
            return sheet;
        }
    }
}
