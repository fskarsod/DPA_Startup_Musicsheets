using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Model.Enum;

namespace DPA_Musicsheets.Core.Builder.Interface
{
    public interface INoteBuilder
    {
        INoteBuilder SetDuration(int duration);

        INoteBuilder HasDot(bool hasDot = true);

        INoteBuilder SetPitch(Pitch pitch);

        INoteBuilder SetAccidental(Accidental accidental);

        INoteBuilder SetOctave(int octave);
    }
}
