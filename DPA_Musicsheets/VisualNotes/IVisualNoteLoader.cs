using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using PSAMControlLibrary;

namespace DPA_Musicsheets.VisualNotes
{
    public delegate void LoadVisualNotes(IEnumerable<MusicalSymbol> symbols);

    public interface IVisualNoteLoader
    {
        event LoadVisualNotes LoadVisualNotes;
    }
}
