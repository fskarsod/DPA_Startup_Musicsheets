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

        public override string ToLilypond()
        {
            string octaveString = "", accidentalString;

            // convert Pitch
            var pitchString = System.Enum.GetName(typeof(Pitch), Pitch).ToLower();

            // convert Accidental
            if (Accidental == Accidental.Flat)
                accidentalString = "es";
            else if (Accidental == Accidental.Sharp)
                accidentalString = "is";
            else
                accidentalString = "";

            // Convert octave
            for (var i = 0; i < Math.Abs(Octave); i++)
                octaveString += Octave > 0 ? "\'" : ",";

            return pitchString + accidentalString + octaveString + Duration + (HasDot ? "." : "");
        }
    }
}