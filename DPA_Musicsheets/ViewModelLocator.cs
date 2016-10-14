using System.Collections.Generic;
using System.Windows.Input;
using DPA_Musicsheets.MidiControl;
using DPA_Musicsheets.Shortcut;
using DPA_Musicsheets.Util;
using DPA_Musicsheets.ViewModel;

namespace DPA_Musicsheets
{
    public class ViewModelLocator
    {
        private ShortcutHandler ShortcutHandler { get; } = new ShortcutHandler(new Dictionary<IEnumerable<Key>, string>(new EnumerableKeyEqualityComparer())
        {
            { new [] { Key.LeftCtrl, Key.S }, "SaveFile" },
            { new [] { Key.LeftCtrl, Key.S, Key.P }, "SavePDF" },
            { new [] { Key.LeftCtrl, Key.O }, "OpenFile" },
            { new [] { Key.LeftCtrl, Key.M }, "PlayMidi" },
            { new [] { Key.LeftAlt, Key.C }, "InsertTreble" },
            { new [] { Key.LeftAlt, Key.S }, "InsertTempo120" },
            { new [] { Key.LeftCtrl, Key.T }, "InsertDefaultTimeSignature" },
            { new [] { Key.LeftCtrl, Key.T, Key.D4 }, "Insert4/4TimeSignature" },
            { new [] { Key.LeftCtrl, Key.T, Key.D3 }, "Insert3/4TimeSignature" },
            { new [] { Key.LeftCtrl, Key.T, Key.D6 }, "Insert6/8TimeSignature" }
        }, new SaveFileShortcut(null));

        public IMidiPlayerControl MidiPlayerControl => new MidiPlayerControl();
        public IDialogService DialogService => new DialogService();

        private IMemento<EditorMemento> _editorMemento;
        public IMemento<EditorMemento> EditorMemento => _editorMemento ?? (_editorMemento = new EditorMemento(string.Empty));

        private EditorViewModel _editorViewModel;
        public EditorViewModel EditorViewModel => _editorViewModel ?? (_editorViewModel = new EditorViewModel(EditorMemento, null));

        private MidiButtonSetVieWModel _midiButtonSetVieWModel;
        public MidiButtonSetVieWModel MidiButtonSetVieWModel => _midiButtonSetVieWModel ?? (_midiButtonSetVieWModel = new MidiButtonSetVieWModel(MidiPlayerControl, DialogService));

        private MainWindowViewModel _mainViewModel;
        public MainWindowViewModel MainWindowViewModel => _mainViewModel ?? (_mainViewModel = new MainWindowViewModel(MidiButtonSetVieWModel, EditorViewModel, ShortcutHandler));
    }
}
