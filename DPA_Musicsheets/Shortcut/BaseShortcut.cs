using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DPA_Musicsheets.Shortcut
{
    public interface IShortcut : IDisposable
    {
        IShortcut Successor { get; set; }

        string Key { get; }

        bool Execute(string key);
    }

    public abstract class BaseShortcut : IShortcut
    {
        public IShortcut Successor { get; set; }

        public abstract string Key { get; }

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
