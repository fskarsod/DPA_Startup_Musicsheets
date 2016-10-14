using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Shortcut
{
    public class InsertSixEightTimeSigShortcut : BaseInsertShortcut
    {
        public InsertSixEightTimeSigShortcut(IShortcut successor, IMemento<EditorMemento> editorMemento)
            : base(successor, editorMemento)
        { }

        public override string Key => "Insert6/8TimeSignature";

        public override string Insertion => "\\time 6/8\r\n";
    }
}
