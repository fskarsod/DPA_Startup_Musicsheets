using System;
using System.Collections.Generic;
using System.Linq;
using DPA_Musicsheets.Core.Interface;

namespace DPA_Musicsheets.Core.Model
{
    public class Bar : IMusicComponentProvider
    {
        public TimeSignature TimeSignature { get; set; }

        public Tempo Tempo { get; set; }

        public IList<IMusicComponent> MusicComponents { get; set; }

        public Bar()
        {
            MusicComponents = new List<IMusicComponent>();
        }

        public IEnumerable<IMusicComponent> GetMusicComponents()
        {
            return MusicComponents;
        }

        public string ToLilypond()
        {
            var barString = "";

            if ((TimeSignature != null) && (Tempo != null))
                barString += TimeSignature.ToLilypond() + Environment.NewLine + Tempo.ToLilypond() + " {" + Environment.NewLine;
            else if (TimeSignature != null)
                barString += TimeSignature.ToLilypond() + " {" + Environment.NewLine;
            else if (Tempo != null)
                barString += Tempo.ToLilypond() + " {" + Environment.NewLine;

            if ((TimeSignature == null) && (Tempo == null))
                barString =
                    MusicComponents.Aggregate(barString, (current, c) => current + c.ToLilypond() + " ").TrimEnd() + Environment.NewLine;

            if ((TimeSignature != null) || (Tempo != null))
                barString += "}" + Environment.NewLine;

            return barString;
        }
    }
}