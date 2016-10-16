using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Command;
using DPA_Musicsheets.IoC;
using DPA_Musicsheets.MidiControl;
using DPA_Musicsheets.Shortcut;

namespace DPA_Musicsheets
{
    public class ShortcutChain : IShortcut
    {
        private readonly IShortcut _initial;

        public IShortcut Successor
        {
            get { return _initial.Successor; }
            set { _initial.Successor = value; }
        }

        public string Key => _initial.Key;

        public ShortcutChain(IContainer container)
        {
            _initial = new Initializer().Initialize(container);
        }

        public bool Execute(string key)
        {
            return _initial.Execute(key);
        }

        public void Dispose()
        {
            _initial.Dispose();
        }

        internal class Initializer
        {
            private static IEnumerable<IShortcut> Instances(IContainer container)
            {
                yield return container.Resolve<SaveFileShortcut>();
                yield return container.Resolve<SaveFileShortcut>();
                yield return container.Resolve<SavePdfShortcut>();
                yield return container.Resolve<OpenFileShortcut>();
                yield return container.Resolve<PlayMidiShortcut>();
                yield return container.Resolve<InsertTrebleShortcut>();
                yield return container.Resolve<InsertTempoShortcut>();
                yield return container.Resolve<InsertDefaultTimeSigShortcut>();
                yield return container.Resolve<InsertFourFourTimeSigShortcut>();
                yield return container.Resolve<InsertThreeFourTimeSigShortcut>();
                yield return container.Resolve<InsertSixEightTimeSigShortcut>();
            }

            public IShortcut Initialize(IContainer container)
            {
                using (var enumerator = Instances(container).GetEnumerator())
                {
                    enumerator.MoveNext();
                    var initial = enumerator.Current;
                    var successor = initial;
                    while (enumerator.MoveNext())
                    {
                        successor.Successor = enumerator.Current;
                        successor = enumerator.Current;
                    }
                    return initial;
                }
            }
        }
    }
}
