using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPA_Musicsheets.Core.Model
{
    public interface IMusicFragment
    {
        Fraction Length { get; }

        Fraction TimeSignature { get; }
    }
}
