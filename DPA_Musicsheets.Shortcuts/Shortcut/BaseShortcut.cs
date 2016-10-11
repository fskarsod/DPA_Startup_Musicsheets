using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DPA_Musicsheets.Shortcuts.Interface;

namespace DPA_Musicsheets.Shortcuts.Shortcut
{
    public abstract class BaseShortcut : IShortcut
    {
        protected IShortcut Successor { get; }

        public abstract string Key { get; } 

        protected BaseShortcut(IShortcut successor)
        {
            Successor = successor;
        }

        public virtual bool CanExecute(string key) { return key != null && Key.Equals(key); }

        public abstract bool OnExecute(string key);

        public bool Execute(string key)
        {
            return CanExecute(key)
                ? OnExecute(key)
                : (Successor?.Execute(key) ?? false);
        }

        public void Dispose()
        {
            Successor?.Dispose();
            OnDispose();
        }

        protected virtual void OnDispose()
        { }
    }
}
