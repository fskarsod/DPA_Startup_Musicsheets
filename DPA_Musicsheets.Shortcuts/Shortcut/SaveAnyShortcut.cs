using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Shortcuts.Interface;

namespace DPA_Musicsheets.Shortcuts.Shortcut
{
    public class SaveAnyShortcut : BaseShortcut
    {
        public SaveAnyShortcut(IShortcut successor)
            : base(successor)
        { }

        public override string Key { get; } = "SaveAny";

        public override bool OnExecute(string key)
        {
            // todo: display generic Save dialog with Midi- and Lilypond-options.
            throw new NotImplementedException();
        }
    }
}
