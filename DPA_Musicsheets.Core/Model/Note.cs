using System;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Model.Enum;

namespace DPA_Musicsheets.Core.Model
{
    public class Note : BaseNote
    {
        public Pitch Pitch { get; set; }

        public Accidental Accidental { get; set; }

        public int Octave { get; set; }

        public override void Accept(IMusicComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}