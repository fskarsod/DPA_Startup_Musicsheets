using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Shortcut
{
    public class InsertThreeFourTimeSigShortcut : BaseInsertShortcut
    {
        public InsertThreeFourTimeSigShortcut(IShortcut successor, IMemento<EditorMemento> editorMemento)
            : base(successor, editorMemento)
        { }

        public override string Key => "Insert3/4TimeSignature";

        public override string Insertion => "\\time 3/4\r\n";
    }
}
