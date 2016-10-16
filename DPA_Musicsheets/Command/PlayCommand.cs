using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DPA_Musicsheets.MidiControl;

namespace DPA_Musicsheets.Command
{
    public interface IPlayCommand : ICommand
    { }

    public class PlayCommand : BaseCommandWpf, IPlayCommand
    {
        private readonly IMidiPlayerControl _midiPlayerControl;

        public PlayCommand(IMidiPlayerControl midiPlayerControl)
        {
            _midiPlayerControl = midiPlayerControl;
        }

        public override void Execute(object parameter)
        {
            _midiPlayerControl.Play();
        }
    }
}
