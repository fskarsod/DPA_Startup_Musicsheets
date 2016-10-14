using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Shortcut
{
    public class InsertTrebleShortcut : BaseInsertShortcut
    {
        public InsertTrebleShortcut(IShortcut successor, IMemento<EditorMemento> editorMemento)
            : base(successor, editorMemento)
        { }

        public override string Key => "InsertTreble";

        public override string Insertion => "\\clef treble\r\n";
    }
}
