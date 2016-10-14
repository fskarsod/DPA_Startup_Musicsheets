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
    public class WindowClosingCommand : BaseCommandWpf
    {
        private IDictionary<int, ICommand> WindowClosingDictionary => new Dictionary<int, ICommand>
        {
            { 7, new RelayCommand(OnSave) },    // no
            { 6, new RelayCommand(OnExit) },    // yes
            { 2, new RelayCommand(OnCancel) }   // cancel
        };

        private readonly IContentStorage _contentStorage;

        private readonly IDialogService _dialogService;

        public WindowClosingCommand(IContentStorage contentStorage, IDialogService dialogService)
        {
            _contentStorage = contentStorage;
            _dialogService = dialogService;
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
            if (WindowClosingDictionary.ContainsKey(result))
            {
                WindowClosingDictionary[result].Execute(e);
            }
            else
            {
                // Something went wrong, cancel closing the application.
                OnCancel(e);
            }
        }

        #region commands
        public void OnExit(object parameter)
        { }

        public void OnSave(object parameter)
        {
            if (!_contentStorage.Save()) // save unsuccessful OR cancel
            {
                OnCancel(parameter);
            }
        }

        public void OnCancel(object parameter)
        {
            var cancelEventArgs = parameter as CancelEventArgs;
            if (cancelEventArgs != null)
                cancelEventArgs.Cancel = true;
        }
        #endregion
    }
}
