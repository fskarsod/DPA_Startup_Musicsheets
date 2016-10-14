using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.MidiControl
{
    public interface IMidiPlayerControl : IDisposable
    {
        void Play(string location);

        void Stop();

        bool IsPlaying { get; }
    }

    public class MidiPlayerControl : IMidiPlayerControl
    {
        private readonly OutputDevice _outputDevice;

        private MidiPlayer _player;

        public bool IsPlaying { get; private set; }

        public MidiPlayerControl()
        {
            _outputDevice = new OutputDevice(0);
        }

        public void Play(string location)
        {
            ResetPlayer(new MidiPlayer(_outputDevice));
            _player.StoppedPlaying += (sender, evt) => IsPlaying = false; // Midi finished playing rather than stop.
            _player.Play(location);
            IsPlaying = true;
        }

        public void Stop()
        {
            IsPlaying = false;
            ResetPlayer(null);
        }

        private void ResetPlayer(MidiPlayer newPlayer)
        {
            _player?.Dispose();
            _player = newPlayer;
            _outputDevice.Reset();
        }

        public void Dispose()
        {
            _player?.Dispose();
            _outputDevice?.Dispose();
        }
    }
}
