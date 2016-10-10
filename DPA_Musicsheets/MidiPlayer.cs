using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;

namespace DPA_Musicsheets
{
    public class MidiPlayer : IDisposable
    {
        private readonly OutputDevice _outDevice;

        // De sequencer maakt het mogelijk om een sequence af te spelen.
        // Deze heeft een timer en geeft events op de juiste momenten.
        private readonly Sequencer _sequencer;

        // De inhoud voor de midi file. Hier zitten onder andere tracks en metadata in.
        private Sequence _sequence;

        public event EventHandler StoppedPlaying;

        public MidiPlayer(OutputDevice outputDevice)
        {
            _outDevice = outputDevice;
            _sequencer = new Sequencer();
            _sequencer.Stopped += (sender, arg) => StoppedPlaying?.Invoke(this, EventArgs.Empty);
            // _sequencer.PlayingCompleted += (sender, arg) => StoppedPlaying?.Invoke(this, EventArgs.Empty);

            // Wanneer een channelmessage langskomt sturen we deze direct door naar onze audio.
            // Channelmessages zijn tonen met commands als NoteOn en NoteOff
            // In midi wordt elke noot gespeeld totdat NoteOff is benoemd. Wanneer dus nooit een NoteOff komt nadat die een NoteOn heeft gehad
            // zal deze note dus oneindig lang blijven spelen.
            _sequencer.ChannelMessagePlayed += ChannelMessagePlayed;

            // Wanneer de sequence klaar is moeten we alles closen en stoppen.
            _sequencer.PlayingCompleted += (playingSender, playingEvent) =>
            {
                _sequencer.Stop();
            };
        }

        public void Play(string midiFileLocation)
        {
            _sequence = new Sequence();
            _sequence.LoadCompleted += OnSequenceLoadCompleted;
            _sequence.LoadAsync(midiFileLocation);
        }

        public void Play(Sequence sequence)
        {
            this._sequence = sequence;
            this._sequencer.Sequence = this._sequence;
            StartPlaying();
        }
        
        private void OnSequenceLoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            _sequencer.Sequence = _sequence;
            StartPlaying();
        }

        private void StartPlaying()
        {
            _sequencer.Start();
        }

        private void ChannelMessagePlayed(object sender, ChannelMessageEventArgs e)
        {
            _outDevice.Send(e.Message);
        }

        public void Dispose()
        {
            _sequencer.Stop();
            _sequencer.Dispose();
        }
    }
}
