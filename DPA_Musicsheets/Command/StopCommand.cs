using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DPA_Musicsheets.MidiControl;

namespace DPA_Musicsheets.Command
{
    public interface IStopCommand : ICommand
    { }

    public class StopCommand : BaseCommandWpf, IStopCommand
    {
        private readonly IMidiPlayerControl _midiPlayerControl;

        public StopCommand(IMidiPlayerControl midiPlayerControl)
        {
            _midiPlayerControl = midiPlayerControl;
        }

        public override bool CanExecute(object parameter) => _midiPlayerControl.IsPlaying;

        public override void Execute(object parameter)
        {
            _midiPlayerControl.Stop();
        }
    }
}
