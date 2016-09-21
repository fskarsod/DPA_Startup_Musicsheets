using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.MidiPlugin.Util;
using Sanford.Multimedia.Midi;
using Track = DPA_Musicsheets.Core.Model.Track;

namespace DPA_Musicsheets.MidiPlugin.Grouping
{
    public class MiscGroup : MidiGroup
    {
        public MidiEvent MidiEvent { get; set; }

        // public override int PreviousEventAbsoluteTickCount { get { return MidiEvent.AbsoluteTicks; } }

        public MiscGroup(TrackMeta trackMeta, MidiEvent @event)
            : base(trackMeta)
        {
            if (@event == null)
                throw new ArgumentNullException("@event");

            MidiEvent = @event;
        }

        public override void CommitEvent(Track track)
        {
            // nothing to do.
        }
    }
}
