using System.Collections.Generic;

namespace DPA_Musicsheets.Core.Model
{
    public class Ending
    {
        public IList<Bar> Bars { get; set; }

        public int Repeats { get; set; }

        public Ending()
        {
            Bars = new List<Bar>();
        }
    }
}
