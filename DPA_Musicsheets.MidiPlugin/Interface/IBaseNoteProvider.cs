using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Model;

namespace DPA_Musicsheets.MidiPlugin.Interface
{
    public interface IBaseNoteProvider
    {
        IEnumerable<BaseNote> GetBaseNotes();
    }
}
