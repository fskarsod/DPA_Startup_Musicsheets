using System.Collections.Generic;
using System.Windows.Input;
using DPA_Musicsheets.Command;
using DPA_Musicsheets.IoC;
using DPA_Musicsheets.MidiControl;
using DPA_Musicsheets.Shortcut;
using DPA_Musicsheets.Util;
using DPA_Musicsheets.ViewModel;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets
{
    public class ViewModelLocator
    {
        public EditorViewModel EditorViewModel => IoCContainer.Instance.Resolve<EditorViewModel>();

        public MidiButtonSetVieWModel MidiButtonSetVieWModel => IoCContainer.Instance.Resolve<MidiButtonSetVieWModel>();

        public MainWindowViewModel MainWindowViewModel => IoCContainer.Instance.Resolve<MainWindowViewModel>();
    }
}
