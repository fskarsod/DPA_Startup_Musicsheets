using DPA_Musicsheets.MidiPlugin.Util;

using TrackModel = DPA_Musicsheets.Core.Model.Track;

namespace DPA_Musicsheets.MidiPlugin.Grouping
{
    public abstract class MidiGroup
    {
        protected TrackMeta TrackMeta { get; private set; }

        protected MidiGroup(TrackMeta trackMeta)
        {
            TrackMeta = trackMeta;
        }

        public abstract void CommitEvent(TrackModel track);
    }
}
