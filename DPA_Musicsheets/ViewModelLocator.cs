using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DPA_Musicsheets.Shortcuts;
using DPA_Musicsheets.Shortcuts.Shortcut;
using DPA_Musicsheets.Shortcuts.Util;
using DPA_Musicsheets.ViewModel;

namespace DPA_Musicsheets
{
    public class IoCContainer
    {
        private readonly IDictionary<Type, object> _pairs;

        public IoCContainer()
        {
            _pairs = new Dictionary<Type, object>();
        }

        public void Register<TAbstract, TImplement>(TImplement instance)
            where TImplement : TAbstract
        {
            _pairs.Add(typeof(TAbstract), instance);
        }

        public TAbstract Get<TAbstract>()
            where TAbstract : class
        {
            var type = typeof(TAbstract);
            return _pairs.ContainsKey(type)
                ? _pairs[type] as TAbstract
                : null;
        }
    }

    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            var container = new IoCContainer();
        }

        private ShortcutHandler ShortcutHandler { get; } = new ShortcutHandler(new Dictionary<IEnumerable<Key>, string>(new EnumerableKeyEqualityComparer())
        {
            { new [] { Key.LeftCtrl, Key.S }, "SaveFile" },
            { new [] { Key.LeftCtrl, Key.S, Key.P }, "SavePDF" },
            { new [] { Key.LeftCtrl, Key.O }, "OpenFile" },
            { new [] { Key.LeftCtrl, Key.M }, "PlayMidi" },
            { new [] { Key.LeftAlt, Key.C }, "InsertTreble" }, // G-Clef
            { new [] { Key.LeftAlt, Key.S }, "InsertTempo120" }, // ???
            { new [] { Key.LeftCtrl, Key.T }, "InsertDefaultTimeSignature" }, // 4/4
            { new [] { Key.LeftCtrl, Key.T, Key.D4 }, "Insert4/4TimeSignature" },
            { new [] { Key.LeftCtrl, Key.T, Key.D3 }, "Insert3/4TimeSignature" },
            { new [] { Key.LeftCtrl, Key.T, Key.D6 }, "Insert6/4TimeSignature" }
        }, new SaveAnyShortcut(null));

        private EditorViewModel _editorViewModel;
        public EditorViewModel EditorViewModel => _editorViewModel ?? (_editorViewModel = new EditorViewModel());

        private MidiButtonSetVieWModel _midiButtonSetVieWModel;
        public MidiButtonSetVieWModel MidiButtonSetVieWModel => _midiButtonSetVieWModel ?? (_midiButtonSetVieWModel = new MidiButtonSetVieWModel());

        private MainWindowViewModel _mainViewModel;
        public MainWindowViewModel MainWindowViewModel => _mainViewModel ?? (_mainViewModel = new MainWindowViewModel(MidiButtonSetVieWModel, EditorViewModel, ShortcutHandler));
    }
}
