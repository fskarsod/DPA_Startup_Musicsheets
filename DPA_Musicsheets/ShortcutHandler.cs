using System;
using System.Collections.Generic;
using System.Windows.Input;
using DPA_Musicsheets.Shortcut;

namespace DPA_Musicsheets
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
            return key != Key.System    // Ignore System because what even is this key?
                && _keys.Add(key)       // Require unique key
                && Handle();            // Handler returns success
        }

        public void RemoveKey(Key key)
        {
            _keys.Remove(key);
        }

        private bool Handle()
        {
            return _keyLinkerDictionary.ContainsKey(_keys)
                && _shortcut.Execute(_keyLinkerDictionary[_keys]);
        }

        public void Dispose()
        {
            _shortcut.Dispose();
        }
    }
}
