using System;
using Microsoft.Win32;
using PSAMControlLibrary;
using Sanford.Multimedia.Midi;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DPA_Musicsheets.Util;

namespace DPA_Musicsheets
{
    public class MuhDataContext
    {
        public ObservableCollection<MidiTrack> Tracks { get; set; }
        
        public string Text { get; set; }

        public ICommand Swappo { get; set; }

        public MuhDataContext()
        {
            Text = string.Empty;
            Swappo = new RelayCommand<string>(args =>
            {
                MessageBox.Show("SWAPPO");
            },
            args => Text.Length > 0);
        }

        // VVVVV LEGACY VVVVV
        //private string _text;
        //public string Text { get { return _text; } set { _text = value; RaisePropertyChanged("Text"); } }

        //public event PropertyChangedEventHandler PropertyChanged;

        //private void RaisePropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MidiPlayer _player;
        public ObservableCollection<MidiTrack> MidiTracks { get; private set; }

        // De OutputDevice is een midi device of het midikanaal van je PC.
        // Hierop gaan we audio streamen.
        // DeviceID 0 is je audio van je PC zelf.
        private readonly OutputDevice _outputDevice = new OutputDevice(0);

        public MainWindow()
        {
            this.MidiTracks = new ObservableCollection<MidiTrack>();
            _delayedActionHandler = new DelayedActionHandler(GeneratorDelay);
            DataContext = new MuhDataContext { Tracks = MidiTracks };
            InitializeComponent();
            FillPSAMViewer();
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

        #region CONTROLS
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{(DataContext as MuhDataContext)?.Text ?? "ASBHDJKSAD"}");
            _player?.Dispose();
            _player = new MidiPlayer(_outputDevice);
            _player.Play(FilePathTextBox.Text);
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Midi Files(.mid)|*.mid" };
            if (openFileDialog.ShowDialog() == true)
            {
                FilePathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            _player?.Dispose();
        }

        private void btn_ShowContent_Click(object sender, RoutedEventArgs e)
        {
            ShowMidiTracks(MidiReader.ReadMidi(FilePathTextBox.Text));
        }

        private void ShowMidiTracks(IEnumerable<MidiTrack> midiTracks)
        {
            MidiTracks.Clear();
            foreach (var midiTrack in midiTracks)
            {
                MidiTracks.Add(midiTrack);
            }

            TabCtrlMidiContent.SelectedIndex = 0;
        }
        #endregion

        #region SAVE BEFORE CLOSE
        // todo: generator save states
        private int _saveCode = 1; // todo: pseudo-code
        private int _getCurrentSaveCode = 0; // todo: pseudo-code

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon
            if (_saveCode != _getCurrentSaveCode)
            {
                var result = MessageBox.Show("Do you want to exit without saving", "EXITOR", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.No:
                        // todo: save sequence and quit immediately, so no cancel
                        goto case MessageBoxResult.Yes; // :o goto (explicit fallthrough)
                    case MessageBoxResult.Yes:
                        CloseApplication();
                        break;
                    default: // implicit cancel
                        e.Cancel = true;
                        break;
                }
            }
            else
            {
                CloseApplication();
            }
        }

        private void CloseApplication()
        {
            _outputDevice?.Close();
            _player?.Dispose();
        }
        #endregion

        #region DELAYED LILYPOND GENERATION
        private const double GeneratorDelay = 1.5D;

        private readonly DelayedActionHandler _delayedActionHandler;

        private int _generatorHashCode;

        private void ItsMyBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextChanged();
        }

        private void TextChanged()
        {
            _delayedActionHandler.Run(() =>
            {
                if (_generatorHashCode != 0)
                {
                    MessageBox.Show("1.5 second have passed, but shit is generating.");
                }
                else
                {
                    MessageBox.Show("1.5 second have passed, app is starting generation.");
                    LilyPondGenerator();
                }
            });
        }

        private void LilyPondGenerator()
        {
            _generatorHashCode = ItsMyBox.Text.GetHashCode();
            new DelayedActionHandler(3d).Run(() => // Replace this line with the LilyPond-Generation code.
            {
                var newGenHashCode = ItsMyBox.Text.GetHashCode();
                if (newGenHashCode != _generatorHashCode) // regenerate, because user is a fuckwit and changes shit.
                {
                    LilyPondGenerator();
                }
                else
                {
                    _generatorHashCode = 0;
                }
            });
        }
        #endregion
    }
}
