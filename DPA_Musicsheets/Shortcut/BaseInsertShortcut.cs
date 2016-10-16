using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Command;

namespace DPA_Musicsheets.Shortcut
{
    public abstract class BaseInsertShortcut : BaseCommandShortcut<IInsertCommand>
    {
        public abstract string Insertion { get; }

        protected BaseInsertShortcut(IInsertCommand command)
            : base(command)
        { }

        public override bool OnExecute(string key)
        {
            Command.Execute(Insertion);
            return true;
        }
    }
}
