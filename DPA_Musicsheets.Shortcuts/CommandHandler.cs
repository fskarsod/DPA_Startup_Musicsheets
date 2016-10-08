using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DPA_Musicsheets.Shortcuts
{
    public class CommandHandler
    {
        private IList<Key> _keys;

        public bool AddKey(Key key) => false;

        public bool RemoveKey(Key key) => false;

        private bool Handle() => false;
    }
}
