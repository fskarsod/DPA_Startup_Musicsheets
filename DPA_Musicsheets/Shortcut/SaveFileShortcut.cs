using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Command;

namespace DPA_Musicsheets.Shortcut
{
    public class SaveFileShortcut : BaseCommandShortcut<ISaveFileCommand>
    {
        public override string Key { get; } = "SaveAny";

        public SaveFileShortcut(ISaveFileCommand command)
            : base(command)
        { }
    }
}
