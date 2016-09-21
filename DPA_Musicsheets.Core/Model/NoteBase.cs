using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPA_Musicsheets.Core.Model
{
    public abstract class NoteBase : IMusicFragment
    {
        public Fraction Length { get; set; }

        public Fraction TimeSignature { get; set; }
    }
}
