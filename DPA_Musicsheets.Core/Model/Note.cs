using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DPA_Musicsheets.Core.Model;
using DPA_Musicsheets.Core.Util;

namespace DPA_Musicsheets.Core.Model
{
    public class Note : NoteBase
    {
        public Type Type { get; set; }

        public Modifier Modifier { get; set; }

        public ushort Octave { get; set; }

        public bool IsChord { get; set; }

        public override string ToString()
        {
            return string.Format("{0}{1}", Type, NoteHelper.ModifierStringified(Modifier));
            // return string.Format("{0}{1}", Type, NoteHelper.ModifierStringified(Modifier), Octave, IsChord);
        }
    }

    public enum Type
    {
        C,
        D,
        E,
        F,
        G,
        A,
        B
    }

    public enum Modifier
    {
        Sharp,
        Flat,
        Natural
    }
}
