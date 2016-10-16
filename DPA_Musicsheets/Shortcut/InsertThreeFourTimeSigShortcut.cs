using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Command;

namespace DPA_Musicsheets.Shortcut
{
    public class InsertThreeFourTimeSigShortcut : BaseInsertShortcut
    {
        public InsertThreeFourTimeSigShortcut(IInsertCommand insertCommand)
            : base(insertCommand)
        { }

        public override string Key => "Insert3/4TimeSignature";

        public override string Insertion => "\\time 3/4\r\n";
    }
}
