using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Shortcut
{
    public class InsertDefaultTimeSigShortcut : BaseInsertShortcut
    {
        public InsertDefaultTimeSigShortcut(IShortcut successor, IMemento<EditorMemento> editorMemento)
            : base(successor, editorMemento)
        { }

        public override string Key => "InsertDefaultTimeSignature";

        public override string Insertion => "\\time 4/4\r\n";
    }
}
