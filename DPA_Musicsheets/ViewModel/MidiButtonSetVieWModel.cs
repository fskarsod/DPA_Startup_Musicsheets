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
using DPA_Musicsheets.Util;
using Microsoft.Win32;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.ViewModel
{
    public class MidiButtonSetVieWModel : BaseViewModel
    {
        // De OutputDevice is een midi device of het midikanaal van je PC.
        // Hierop gaan we audio streamen.
        // DeviceID 0 is je audio van je PC zelf.
        private readonly OutputDevice _outputDevice;

        private MidiPlayer _player;

        public ObservableCollection<MidiTrack> MidiTracks { get; }

        private string _fileLocation;

        public string FileLocation
        {
            get
            {
                return _fileLocation;
            }
            set
            {
                _fileLocation = value;
                RaisePropertyChanged("FileLocation");
            }
        }

        public ICommand Play { get; set; }

        public ICommand Stop { get; set; }

        public ICommand Open { get; set; }

        public ICommand Show { get; set; }

        // State
        private bool _isPlaying;

        public MidiButtonSetVieWModel()
        {
            FileLocation = "../../../ten desires.mid";
            MidiTracks = new ObservableCollection<MidiTrack>();
            _outputDevice = new OutputDevice(0);

            Play = new RelayCommand(OnPlay, HasFileLocation);
            Stop = new RelayCommand(OnStop, CanStop);
            Open = new RelayCommand(OnOpen);
            Show = new RelayCommand(OnShow, HasFileLocation);
        }
        
        private void OnPlay(object args)
        {
            ResetPlayer(new MidiPlayer(_outputDevice));
            _player.StoppedPlaying += (sender, evt) => _isPlaying = false; // Midi finished playing rather than stop.
            _player.Play(FileLocation);
            _isPlaying = true;
        }

        private void OnStop(object args)
        {
            _isPlaying = false;
            ResetPlayer(null);
        }

        private void ResetPlayer(MidiPlayer newPlayer)
        {
            _player?.Dispose();
            _player = newPlayer;
            _outputDevice.Reset();
        }

        private bool CanStop(object args)
        {
            return _isPlaying;
        }

        private void OnOpen(object args)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Midi Files(.mid)|*.mid" };
            if (openFileDialog.ShowDialog() == true)
            {
                FileLocation = openFileDialog.FileName;
            }
        }

        private void OnShow(object args)
        {
            MidiTracks.Clear();
            foreach (var midiTrack in MidiReader.ReadMidi(FileLocation))
            {
                MidiTracks.Add(midiTrack);
            }
        }

        private bool HasFileLocation(object args)
        {
            return FileLocation?.Length > 0;
        }

        public override void Dispose()
        {
            _player?.Dispose();
            _outputDevice?.Dispose();
        }
    }
}
