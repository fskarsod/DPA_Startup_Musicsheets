using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.MidiControl
{
    public interface IMidiPlayerControl : IDisposable
    {
        bool IsPlaying { get; }

        void Play();

        void Stop();
    }

    public class MidiPlayerControl : IMidiPlayerControl
    {
        private readonly OutputDevice _outputDevice;

        private readonly IApplicationContext _applicationContext;
        private readonly IDialogService _dialogService;
        private readonly MidiPlayer _player;

        public bool IsPlaying { get; private set; }

        public MidiPlayerControl(OutputDevice outputDevice, IApplicationContext applicationContext, IDialogService dialogService)
        {
            _outputDevice = outputDevice;
            _applicationContext = applicationContext;
            _dialogService = dialogService;

            _player = new MidiPlayer(_outputDevice);
            _player.StoppedPlaying += (sender, evt) => IsPlaying = false; // Midi finished playing rather than stop.
        }

        public void Play()
        {
            if (string.IsNullOrEmpty(_applicationContext.FileLocation))
                return;
            Reset();
            try
            {
                _player.Play(_applicationContext.FileLocation);
                IsPlaying = true;
            }
            catch (FileNotFoundException)
            {
                _dialogService.DisplayError("The file could not be played.");
            }
        }

        public void Stop()
        {
            IsPlaying = false;
            Reset();
        }

        private void Reset()
        {
            _player.Reset();
            _outputDevice.Reset();
        }

        public void Dispose()
        {
            _player.Dispose();
            _outputDevice.Dispose();
        }
    }
}
