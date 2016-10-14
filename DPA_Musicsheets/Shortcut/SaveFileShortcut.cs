using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Shortcut
{
    public class SaveFileShortcut : BaseShortcut
    {
        public SaveFileShortcut(IShortcut successor)
            : base(successor)
        { }

        public override string Key { get; } = "SaveAny";

        public override bool OnExecute(string key)
        {
            // todo: display generic Save dialog with Midi- and Lilypond-options.
            string saveLocation = string.Empty;
            string saveFormat = string.Empty;
            // todo: Get currently loaded {Ext.Sheet}.
            // todo: Convert it to {saveFormat}.
            // todo: Save it to {saveLocation}
            throw new NotImplementedException();
        }
    }
}
