using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DPA_Musicsheets.Command;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.MidiControl;
using DPA_Musicsheets.LilypondPlugin.Plugin;
using DPA_Musicsheets.Util;
using DPA_Musicsheets.VisualNotes;
using Microsoft.Win32;
using PSAMControlLibrary;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.ViewModel
{
    public class MidiButtonSetVieWModel : BaseViewModel
    {
        private readonly IApplicationContext _applicationContext;
        
        private readonly IPluginWriter<string> _lilypondPluginReader;
        private readonly IPluginReader<IEnumerable<MusicalSymbol>> _visualnotePluginReader;

        private IMusicalSymbolConsumer _musicalSymbolConsumer;

        public ObservableCollection<MidiTrack> MidiTracks { get; }

        #region public string FileLocation { get; set; } // _midiPlayerControl.Location
        public string FileLocation
        {
            get
            {
                return _applicationContext.FileLocation;
            }
            set
            {
                _applicationContext.FileLocation = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public ICommand Play { get; set; }

        public ICommand Stop { get; set; }

        public ICommand Save { get; set; }

        public ICommand Open { get; set; }

        public ICommand Show { get; set; }

        public MidiButtonSetVieWModel(
            IApplicationContext applicationContext,
            IPluginWriter<string> lilypondPluginReader,
            IPluginReader<IEnumerable<MusicalSymbol>> visualnotePluginReader,
            IPlayCommand playCommand,
            IStopCommand stopCommand,
            IOpenFileCommand openFileCommand,
            ISaveFileCommand saveFileCommand)
        {
            _applicationContext = applicationContext;
            _lilypondPluginReader = lilypondPluginReader;
            _visualnotePluginReader = visualnotePluginReader;
            FileLocation = "../../../ten desires.mid";
            MidiTracks = new ObservableCollection<MidiTrack>();

            Play = playCommand;
            Stop = stopCommand;
            Open = openFileCommand;
            Save = saveFileCommand;
            Show = new RelayCommand(OnShow, IsMidiFileLocation); // MIDI ONLY

            _applicationContext.PropertyChanged += (sender, evt) =>
            {
                if (evt.PropertyName.Equals(nameof(IApplicationContext.FileLocation)))
                    OnPropertyChanged(nameof(IApplicationContext.FileLocation));
            };
        }

        public void SetMusicalSymbolConsumer(IMusicalSymbolConsumer musicalSymbolConsumer)
        {
            _musicalSymbolConsumer = musicalSymbolConsumer;
        }

        #region MIDI MUSIC PLAYER

        private void OnShow(object args)
        {
            if (IsMidiFileLocation(args))
            // OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Sheetmusic Files(*.mid;*.ly)|*.mid;*.ly|Midi Files(.mid)|*.mid|Lilypond Files(.ly)|*.ly" };
            // if (openFileDialog.ShowDialog() == true)
            {
                PopulateTabControl();       // to content
                PopulateIncipitViewer();    // to visual notes
            }
        }

        private void PopulateTabControl()
        {
			MidiTracks.Clear();
            foreach (var midiTrack in MidiReader.ReadMidi(FileLocation))
            {
                MidiTracks.Add(midiTrack);
			}
			
            // switch (FileLocation.Split('.').Last())
            // {
                // case "ly":
                    // var fileContents = File.ReadAllText(FileLocation);
                    // var lilyWriter = new LilypondPluginWriter();
                    // lilyWriter.WriteSheet(fileContents);
                    // break;
                // case "mid":
                    // foreach (var midiTrack in MidiReader.ReadMidi(FileLocation))
                        // MidiTracks.Add(midiTrack);
                    // break;
                // default:
                    // // unsupported filetype
                    // // throw new NotImplementedException(); // TODO: Make a custom exception/state.
                    // return;
            // }
        }

        private void PopulateIncipitViewer()
        {
            var sheet = _lilypondPluginReader.WriteSheet(_applicationContext.EditorMemento.Content);
            var result = _visualnotePluginReader.ReadSheet(sheet);
            _musicalSymbolConsumer.Consume(result);
        }

        private bool IsMidiFileLocation(object args)
        {
            return FileLocation?.Length > 0 && (FileLocation?.EndsWith(".mid") ?? false);
        }
        #endregion
    }
}
