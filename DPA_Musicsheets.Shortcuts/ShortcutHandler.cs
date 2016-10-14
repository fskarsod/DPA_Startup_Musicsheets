using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DPA_Musicsheets.Shortcuts.Interface;

namespace DPA_Musicsheets.Shortcuts
{
    public class ShortcutHandler : IDisposable
    {
        private readonly IDictionary<IEnumerable<Key>, string> _keyLinkerDictionary;
        
        private readonly IShortcut _shortcut;

        private readonly HashSet<Key> _keys;

        public ShortcutHandler(IDictionary<IEnumerable<Key>, string> keyLinkerDictionary, IShortcut shortcut)
        {
            _keyLinkerDictionary = keyLinkerDictionary;
            _shortcut = shortcut;
            _keys = new HashSet<Key>();
        }

        public bool AddKey(Key key)
        {
            return key != Key.System && _keys.Add(key) && Handle();
        }

        public void RemoveKey(Key key)
        {
            _keys.Remove(key);
        }

        private bool Handle()
        {
            var contains = _keyLinkerDictionary.ContainsKey(_keys);
            return contains
                   && _shortcut.Execute(_keyLinkerDictionary[_keys]);
        }

        public void Dispose()
        {
            _shortcut.Dispose();
        }
    }
}
