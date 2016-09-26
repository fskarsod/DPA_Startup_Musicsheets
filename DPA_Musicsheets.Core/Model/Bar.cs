using System.Collections.Generic;
using DPA_Musicsheets.Core.Interface;

namespace DPA_Musicsheets.Core.Model
{
    public class Bar : IMusicComponentProvider
    {
        public TimeSignature TimeSignature { get; set; }

        public IList<IMusicComponent> MusicComponents { get; set; }

        public Bar()
        {
            MusicComponents = new List<IMusicComponent>();
        }

        public IEnumerable<IMusicComponent> GetMusicComponents()
        {
            return MusicComponents;
        }
    }
}