using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Model;

namespace DPA_Musicsheets.MidiPlugin.Util
{
    public class TrackMeta
    {
        public TimeSignature TimeSignature { get; set; }

        public int SequenceDivision { get; set; }
    }
}
