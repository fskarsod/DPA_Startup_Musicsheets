using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DPA_Musicsheets.Core.Interface;

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

        public string ToLilypond()
        {
            return Bars.Aggregate("{", (current, bar) => current + bar.ToLilypond()) + $"}}{Environment.NewLine}";
        }
    }
}
