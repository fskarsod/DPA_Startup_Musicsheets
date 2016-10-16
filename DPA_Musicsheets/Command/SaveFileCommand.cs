using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DPA_Musicsheets.Command
{
    public interface ISaveFileCommand : ICommand
    { }

    public class SaveFileCommand : BaseCommandWpf, ISaveFileCommand
    {
        private readonly IContentStorage _contentStorage;
        private readonly ICancelCommand _cancelCommand;

        public SaveFileCommand(IContentStorage contentStorage, ICancelCommand cancelCommand)
        {
            _contentStorage = contentStorage;
            _cancelCommand = cancelCommand;
        }

        public override void Execute(object parameter)
        {
            if (!_contentStorage.Save()) // save unsuccessful OR cancel
            {
                _cancelCommand.Execute(parameter);
            }
        }
    }
}