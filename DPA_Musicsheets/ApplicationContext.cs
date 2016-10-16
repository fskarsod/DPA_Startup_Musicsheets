using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Annotations;
using DPA_Musicsheets.MidiControl;

namespace DPA_Musicsheets
{
    public interface IApplicationContext : INotifyPropertyChanged
    {
        bool Saved { get; set; }

        string FileLocation { get; set; }

        EditorMemento EditorMemento { get; }
    }

    public class ApplicationContext : IApplicationContext
    {
        private bool _saved;
        public bool Saved { get { return _saved; } set { _saved = value; OnPropertyChanged(); } }

        private string _fileLocation;
        public string FileLocation { get { return _fileLocation; } set { _fileLocation = value; OnPropertyChanged(); } }

        public EditorMemento EditorMemento { get; }

        public ApplicationContext()
        {
            EditorMemento = new EditorMemento();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
