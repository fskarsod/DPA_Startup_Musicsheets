using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DPA_Musicsheets.Command;
using DPA_Musicsheets.MidiControl;
using DPA_Musicsheets.Util;
using DPA_Musicsheets.VisualNotes;
using Microsoft.Win32;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.ViewModel
{
    public class MidiButtonSetVieWModel : BaseViewModel
    {
        private readonly IMidiPlayerControl _midiPlayerControl;

        private readonly IDialogService _dialogService;

        private IMusicalSymbolConsumer _musicalSymbolConsumer;

        public ObservableCollection<MidiTrack> MidiTracks { get; }

        #region public string FileLocation { get; set; } // RaisePropertyChanged
        private string _fileLocation;
        public string FileLocation
        {
            get
            {
                return _fileLocation;
            }
            set
            {
                _fileLocation = value; RaisePropertyChanged("FileLocation");
            }
        }
        #endregion

        public ICommand Play { get; set; }

        public ICommand Stop { get; set; }

        public ICommand Open { get; set; }

        public ICommand Show { get; set; }

        public MidiButtonSetVieWModel(IMidiPlayerControl midiPlayerControl, IDialogService dialogService)
        {
            _midiPlayerControl = midiPlayerControl;
            _dialogService = dialogService;
            FileLocation = "../../../ten desires.mid";
            MidiTracks = new ObservableCollection<MidiTrack>();

            Play = new RelayCommand(OnPlay, HasFileLocation);
            Stop = new RelayCommand(OnStop, CanStop);
            Open = new RelayCommand(OnOpen);
            Show = new RelayCommand(OnShow, HasFileLocation);
        }

        public void SetMusicalSymbolConsumer(IMusicalSymbolConsumer musicalSymbolConsumer)
        {
            _musicalSymbolConsumer = musicalSymbolConsumer;
        }

        #region MIDI MUSIC PLAYER
        private void OnPlay(object args)
        {
            _midiPlayerControl.Play(FileLocation);
        }

        private void OnStop(object args)
        {
            _midiPlayerControl.Stop();
        }

        private bool CanStop(object args)
        {
            return _midiPlayerControl.IsPlaying;
        }

        private void OnOpen(object args)
        {
            FileLocation = _dialogService.DisplayOpen();
        }

        private void OnShow(object args)
        {
            PopulateTabControl();
            PopulateIncipitViewer();
        }

        private void PopulateTabControl()
        {
            MidiTracks.Clear();
            foreach (var midiTrack in MidiReader.ReadMidi(FileLocation))
            {
                MidiTracks.Add(midiTrack);
            }
        }

        private void PopulateIncipitViewer()
        {
            // todo: VisualNotePluginThing
            // _musicalSymbolConsumer.Consume(null);
        }

        private bool HasFileLocation(object args)
        {
            return FileLocation?.Length > 0;
        }
        #endregion

        public override void Dispose()
        {
            _midiPlayerControl.Dispose();
        }
    }
}
