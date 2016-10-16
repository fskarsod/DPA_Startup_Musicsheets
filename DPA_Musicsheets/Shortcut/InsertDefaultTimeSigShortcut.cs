using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Command;

namespace DPA_Musicsheets.Shortcut
{
    public class InsertDefaultTimeSigShortcut : BaseInsertShortcut
    {
        public InsertDefaultTimeSigShortcut(IInsertCommand insertCommand)
            : base(insertCommand)
        { }

        public override string Key => "InsertDefaultTimeSignature";

        public override string Insertion => "\\time 4/4\r\n";
    }
}
