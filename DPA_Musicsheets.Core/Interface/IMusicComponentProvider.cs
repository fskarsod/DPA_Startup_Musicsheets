using System.Collections.Generic;

namespace DPA_Musicsheets.Core.Interface
{
    public interface IMusicComponentProvider
    {
        IEnumerable<IMusicComponent> GetMusicComponents();
    }
}