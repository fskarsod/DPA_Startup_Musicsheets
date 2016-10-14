using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Shortcut
{
    public class InsertTempoShortcut : BaseInsertShortcut
    {
        public InsertTempoShortcut(IShortcut successor, IMemento<EditorMemento> editorMemento)
            : base(successor, editorMemento)
        { }

        public override string Key => "InsertTempo120";

        public override string Insertion => "\\tempo 4 = 120\r\n";
    }
}
