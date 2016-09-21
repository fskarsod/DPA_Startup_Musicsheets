using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPA_Musicsheets.Core.Model
{
    public class Sheet
    {
        public IList<Track> Tracks { get; set; }

        public Sheet()
        {
            Tracks = new List<Track>();
        }
    }
}
