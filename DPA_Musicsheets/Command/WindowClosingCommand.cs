using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DPA_Musicsheets.Command
{
    public interface IWindowClosingCommand : ICommand
    { }

    public class WindowClosingCommand : BaseCommandWpf, IWindowClosingCommand
    {
        private readonly IDictionary<int, ICommand> _windowClosingDictionary;

        private readonly IContentStorage _contentStorage;

        private readonly IDialogService _dialogService;

        public WindowClosingCommand(IContentStorage contentStorage, IDialogService dialogService, ISaveFileCommand saveCommand)
        {
            _contentStorage = contentStorage;
            _dialogService = dialogService;
            _windowClosingDictionary = new Dictionary<int, ICommand>
            {
                { 7, saveCommand },             // no
                { 6, new EmptyCommand() },      // yes
                { 2, new CancelCommand() }      // cancel
            };
        }

        public override void Execute(object parameter)
        {
            if (!_contentStorage.Saved && parameter is CancelEventArgs)
            {
                BeforeExitSequence((CancelEventArgs) parameter);
            }
        }

        private void BeforeExitSequence(CancelEventArgs e)
        {
            var result = _dialogService.DisplayYesNoCancel("Wil je afsluiten zonder op te slaan?", "Afsluiten");
            if (_windowClosingDictionary.ContainsKey(result))
            {
                _windowClosingDictionary[result].Execute(e);
            }
            else
            {
                // Something went wrong, cancel closing the application.
                _windowClosingDictionary[2].Execute(e);
            }
        }
    }
}
