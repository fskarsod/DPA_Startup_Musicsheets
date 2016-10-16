using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSAMControlLibrary;

namespace DPA_Musicsheets.VisualNotes
{
    public interface IMusicalSymbolConsumer
    {
        void Consume(IEnumerable<MusicalSymbol> symbols);
    }
}
