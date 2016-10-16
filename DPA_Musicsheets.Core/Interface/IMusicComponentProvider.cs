using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DPA_Musicsheets.Core.Interface
{
    public interface IMusicComponentProvider
    {
        IEnumerable<IMusicComponent> GetMusicComponents();
        string ToLilypond();
    }
}