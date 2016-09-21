using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPA_Musicsheets.Core.Model
{
    public class Track
    {
        public IList<IMusicFragment> MusicFragments { get; set; }

        public Track()
        {
            MusicFragments = new List<IMusicFragment>();
        }
    }
}
