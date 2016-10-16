using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Command;

namespace DPA_Musicsheets.Shortcut
{
    public class InsertTrebleShortcut : BaseInsertShortcut
    {
        public InsertTrebleShortcut(IInsertCommand insertCommand)
            : base(insertCommand)
        { }

        public override string Key => "InsertTreble";

        public override string Insertion => "\\clef treble\r\n";
    }
}
