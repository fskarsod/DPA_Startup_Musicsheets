using System;
using Microsoft.Win32;
using PSAMControlLibrary;
using Sanford.Multimedia.Midi;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DPA_Musicsheets.Core.Model;
using DPA_Musicsheets.LilypondPlugin.Plugin;
using DPA_Musicsheets.MidiPlugin.Util;
using Note = PSAMControlLibrary.Note;
using TimeSignature = PSAMControlLibrary.TimeSignature;
using DPA_Musicsheets.Command;
using DPA_Musicsheets.Util;
using DPA_Musicsheets.ViewModel;

namespace DPA_Musicsheets
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CancelExit = new RelayCommand(OnCancelExit);
            // FillPSAMViewer();
            // notenbalk.LoadFromXmlFile("Resources/example.xml");
            // Core.Builder.Sample.BuilderSample.Main();
        }

        #region VISUAL NOTE GENERATION
        private void FillPSAMViewer()
        {
            staff.ClearMusicalIncipit();

            // Clef = sleutel
            staff.AddMusicalSymbol(new Clef(ClefType.GClef, 2));
            staff.AddMusicalSymbol(new TimeSignature(TimeSignatureType.Numbers, 4, 4));
            /* 
                The first argument of Note constructor is a string representing one of the following names of steps: A, B, C, D, E, F, G. 
                The second argument is number of sharps (positive number) or flats (negative number) where 0 means no alteration. 
                The third argument is the number of an octave. 
                The next arguments are: duration of the note, stem direction and type of tie (NoteTieType.None if the note is not tied). 
                The last argument is a list of beams. If the note doesn't have any beams, it must still have that list with just one 
                    element NoteBeamType.Single (even if duration of the note is greater than eighth). 
                    To make it clear how beamlists work, let's try to add a group of two beamed sixteenths and eighth:
                        Note s1 = new Note("A", 0, 4, MusicalSymbolDuration.Sixteenth, NoteStemDirection.Down, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Start, NoteBeamType.Start});
                        Note s2 = new Note("C", 1, 5, MusicalSymbolDuration.Sixteenth, NoteStemDirection.Down, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Continue, NoteBeamType.End });
                        Note e = new Note("D", 0, 5, MusicalSymbolDuration.Eighth, NoteStemDirection.Down, NoteTieType.None,new List<NoteBeamType>() { NoteBeamType.End });
                        viewer.AddMusicalSymbol(s1);
                        viewer.AddMusicalSymbol(s2);
                        viewer.AddMusicalSymbol(e); 
            */

            staff.AddMusicalSymbol(new Note("A", 0, 4, MusicalSymbolDuration.Sixteenth, NoteStemDirection.Down, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Start, NoteBeamType.Start }));
            staff.AddMusicalSymbol(new Note("C", 1, 5, MusicalSymbolDuration.Sixteenth, NoteStemDirection.Down, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Continue, NoteBeamType.End }));
            staff.AddMusicalSymbol(new Note("D", 0, 5, MusicalSymbolDuration.Eighth, NoteStemDirection.Down, NoteTieType.Start, new List<NoteBeamType>() { NoteBeamType.End }));
            staff.AddMusicalSymbol(new Barline());

            staff.AddMusicalSymbol(new Note("D", 0, 5, MusicalSymbolDuration.Whole, NoteStemDirection.Down, NoteTieType.Stop, new List<NoteBeamType>() { NoteBeamType.Single }));
            staff.AddMusicalSymbol(new Note("E", 0, 4, MusicalSymbolDuration.Quarter, NoteStemDirection.Up, NoteTieType.Start, new List<NoteBeamType>() { NoteBeamType.Single }) { NumberOfDots = 1 });
            staff.AddMusicalSymbol(new Barline());

            staff.AddMusicalSymbol(new Note("C", 0, 4, MusicalSymbolDuration.Half, NoteStemDirection.Up, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Single }));
            staff.AddMusicalSymbol(
                new Note("E", 0, 4, MusicalSymbolDuration.Half, NoteStemDirection.Up, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Single }) { IsChordElement = true });
            staff.AddMusicalSymbol(
                new Note("G", 0, 4, MusicalSymbolDuration.Half, NoteStemDirection.Up, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Single }) { IsChordElement = true });
            staff.AddMusicalSymbol(new Barline());

            for (var i = 0; i < 20; i++)
                {
                staff.AddMusicalSymbol(new Note("A", 0, 4, MusicalSymbolDuration.Sixteenth, NoteStemDirection.Down, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Start, NoteBeamType.Start }));
            }
        }
        #endregion
        
        #region SAVE BEFORE CLOSE
        private bool _saved = false; // todo: link to save-sequence

        private IDictionary<MessageBoxResult, ICommand> WindowClosingDictionary => new Dictionary<MessageBoxResult, ICommand>
        {
            { MessageBoxResult.No, new RelayCommand(OnSaveBeforeExit) },
            { MessageBoxResult.Yes, new RelayCommand(OnForceExit) }
        };

        private ICommand CancelExit { get; set; }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!_saved)
            {
                BeforeExitSequence(e);
            }
            else
            {
                CloseApplication();
            }
        }

        private void BeforeExitSequence(CancelEventArgs e)
        {
            var result = MessageBox.Show("Do you want to exit without saving", "EXIT", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if (WindowClosingDictionary.ContainsKey(result))
            {
                WindowClosingDictionary[result].Execute(e);
            }
            else
            {
                CancelExit.Execute(e);
            }
        }

        // save-sequence
        private void OnSaveBeforeExit(object parameters)
        {
            var saveFileDialog = new SaveFileDialog { Filter = "Sheetmusic Files(*.mid;*.ly)|*.mid;*.ly|Midi Files(.mid)|*.mid|Lilypond Files(.ly)|*.ly" };
            if (saveFileDialog.ShowDialog() == true)
            {
                var file = saveFileDialog.FileName;
                File.WriteAllText(file, "andansldbaskjdnjkasdjkadkjabdkhbakd"); // todo: get EditorText
                _saved = true;
                CloseApplication();
            }
            else // cancel save dialog -> no exit
            {
                CancelExit.Execute(parameters);
            }
        }

        private void OnForceExit(object parameters)
        {
            CloseApplication();
        }

        private void OnCancelExit(object parameters)
        {
            var cancelEventArgs = parameters as CancelEventArgs;
            if (cancelEventArgs != null)
                cancelEventArgs.Cancel = true;
        }

        #endregion

        private void CloseApplication()
        {
            (DataContext as MainWindowViewModel)?.Dispose();
        }

        private void Window_OnKeyDown(object sender, KeyEventArgs e)
        {
            (DataContext as MainWindowViewModel)?.ShortcutHandler?.AddKey(e.Key);
            // throw new NotImplementedException();
        }

        private void Window_OnKeyUp(object sender, KeyEventArgs e)
        {
            (DataContext as MainWindowViewModel)?.ShortcutHandler?.RemoveKey(e.Key);
            // throw new NotImplementedException();
        }
    }
}
