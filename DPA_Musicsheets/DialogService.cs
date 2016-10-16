using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace DPA_Musicsheets
{
    public interface IDialogService
    {
        string DisplaySave();

        string DisplayOpen();

        int DisplayYesNoCancel(string message, string caption);

        void DisplayInformation(string message);

        void DisplayError(string message);
    }

    public class DialogService : IDialogService
    {
        public string SaveFilter { get; set; } = "Lilypond Files(.ly)|*.ly|PDF Files(.pdf)|*.pdf";

        public string OpenFilter { get; set; } = "Lilypond Files(.ly)|*.ly|Midi Files(.mid)|*.mid";

        public string DisplaySave()
        {
            var saveFileDialog = new SaveFileDialog { Filter = SaveFilter };
            return saveFileDialog.ShowDialog() == true
                ? saveFileDialog.FileName
                : string.Empty;
        }

        public string DisplayOpen()
        {
            var openFileDialog = new OpenFileDialog() { Filter = OpenFilter };
            return openFileDialog.ShowDialog() == true
                ? openFileDialog.FileName
                : string.Empty;
        }

        public int DisplayYesNoCancel(string message, string caption)
        {
            return (int) MessageBox.Show(message, caption, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
        }

        public void DisplayInformation(string message)
        {
            MessageBox.Show(message, "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void DisplayError(string message)
        {
            MessageBox.Show(message, "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
