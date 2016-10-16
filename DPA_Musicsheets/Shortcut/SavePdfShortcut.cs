using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Command;

namespace DPA_Musicsheets.Shortcut
{
    // todo: implement Save PDF
    public class SavePdfShortcut : BaseCommandShortcut<ISaveFileCommand>
    {
        public override string Key => "SavePDF";

        public SavePdfShortcut(ISaveFileCommand command)
            : base(command)
        { }
    }
}
