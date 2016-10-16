using System;
using System.Collections.Generic;
using System.Linq;

namespace DPA_Musicsheets.Core.Model
{
    public class Sheet
    {
        public string Name { get; set; }

        public IList<Track> Tracks { get; set; }

        public string ToLilypond()
        {
            // ignores Name property & only uses 1st track...
            return Tracks.Aggregate("", (current, track) => current + track.ToLilypond());
        }
    }
}