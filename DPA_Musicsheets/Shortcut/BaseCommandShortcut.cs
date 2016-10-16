using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DPA_Musicsheets.Shortcut
{
    public abstract class BaseCommandShortcut<TCommand> : BaseShortcut
        where TCommand : ICommand
    {
        protected readonly TCommand Command;

        protected BaseCommandShortcut(TCommand command)
        {
            Command = command;
        }

        public override bool OnExecute(string key)
        {
            Command.Execute(key);
            return true;
        }
    }
}
