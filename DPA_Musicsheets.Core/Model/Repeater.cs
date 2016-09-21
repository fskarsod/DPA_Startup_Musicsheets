using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPA_Musicsheets.Core.Model
{
    public class Repeater : IMusicFragment
    {
        public Fraction Length { get { return _musicFragments.Aggregate<IMusicFragment, Fraction>(null, (current, note) => current + note.Length); } }

        public Fraction TimeSignature { get { return _musicFragments.Aggregate<IMusicFragment, Fraction>(null, (current, note) => current + note.TimeSignature); } }

        private IMusicFragment[] _musicFragments;
    }
}
