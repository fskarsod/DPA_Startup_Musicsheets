using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Command;

namespace DPA_Musicsheets.Shortcut
{
    public class InsertSixEightTimeSigShortcut : BaseInsertShortcut
    {
        public InsertSixEightTimeSigShortcut(IInsertCommand insertCommand)
            : base(insertCommand)
        { }

        public override string Key => "Insert6/8TimeSignature";

        public override string Insertion => "\\time 6/8\r\n";
    }
}
