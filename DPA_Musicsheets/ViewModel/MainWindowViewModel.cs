using System.Windows.Input;
using DPA_Musicsheets.Command;

namespace DPA_Musicsheets.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MidiButtonSetVieWModel MidiButtonSetVieWModel { get; set; }

        public EditorViewModel EditorViewModel { get; set; }

        public ShortcutHandler ShortcutHandler { get; set; }

        public ICommand WindowClosing { get; set; }

        public MainWindowViewModel(MidiButtonSetVieWModel midiButtonSetVieWModel, EditorViewModel editorViewModel, ShortcutHandler shortcutHandler)
        {
            MidiButtonSetVieWModel = midiButtonSetVieWModel;
            EditorViewModel = editorViewModel;
            ShortcutHandler = shortcutHandler;

            // WindowClosing = new WindowClosingCommand();
        }

        public override void Dispose()
        {
            MidiButtonSetVieWModel?.Dispose();
            EditorViewModel?.Dispose();
            ShortcutHandler?.Dispose();
        }
    }
}
