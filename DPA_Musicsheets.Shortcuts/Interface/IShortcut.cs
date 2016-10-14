using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Shortcuts.Interface
{
    public interface IShortcut : IDisposable
    {
        string Key { get; }

        bool Execute(string key);
    }
}
