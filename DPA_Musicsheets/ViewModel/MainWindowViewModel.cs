using System.Windows.Input;
using DPA_Musicsheets.Command;
using DPA_Musicsheets.VisualNotes;

namespace DPA_Musicsheets.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private IMusicalSymbolConsumer _musicalSymbolConsumer;

        public MidiButtonSetVieWModel MidiButtonSetVieWModel { get; set; }

        public EditorViewModel EditorViewModel { get; set; }

        public ShortcutHandler ShortcutHandler { get; set; }

        public ICommand WindowClosing { get; set; }

        public MainWindowViewModel(MidiButtonSetVieWModel midiButtonSetVieWModel,
            EditorViewModel editorViewModel, ShortcutHandler shortcutHandler,
            IContentLoader contentLoader,
            IWindowClosingCommand windowClosingCommand)
        {
            MidiButtonSetVieWModel = midiButtonSetVieWModel;
            EditorViewModel = editorViewModel;
            ShortcutHandler = shortcutHandler;

            WindowClosing = windowClosingCommand;
            contentLoader.LoadVisualNotes += (symbols) =>
            {
                _musicalSymbolConsumer.Consume(symbols);
            };
        }

        public void SetMusicalSymbolConsumer(IMusicalSymbolConsumer musicalSymbolConsumer)
        {
            _musicalSymbolConsumer = musicalSymbolConsumer;
        }

        public override void Dispose()
        {
            MidiButtonSetVieWModel?.Dispose();
            EditorViewModel?.Dispose();
            ShortcutHandler?.Dispose();
        }
    }
}
