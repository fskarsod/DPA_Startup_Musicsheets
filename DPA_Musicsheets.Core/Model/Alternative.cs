using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Core.Model
{
    public class Alternative
    {
        public IList<Bar> Ending { get; set; }

        public int Repeats { get; set; }
    }
}
