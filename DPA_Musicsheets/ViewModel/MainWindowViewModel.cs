using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using DPA_Musicsheets.Command;
using DPA_Musicsheets.Shortcuts;
using DPA_Musicsheets.Util;
using Microsoft.Win32;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MidiButtonSetVieWModel MidiButtonSetVieWModel { get; set; }

        public EditorViewModel EditorViewModel { get; set; }

        public ShortcutHandler ShortcutHandler { get; set; }

        public MainWindowViewModel(MidiButtonSetVieWModel midiButtonSetVieWModel, EditorViewModel editorViewModel, ShortcutHandler shortcutHandler)
        {
            MidiButtonSetVieWModel = midiButtonSetVieWModel;
            EditorViewModel = editorViewModel;
            ShortcutHandler = shortcutHandler;
        }

        public override void Dispose()
        {
            MidiButtonSetVieWModel?.Dispose();
            EditorViewModel?.Dispose();
            ShortcutHandler?.Dispose();
        }
    }
}
