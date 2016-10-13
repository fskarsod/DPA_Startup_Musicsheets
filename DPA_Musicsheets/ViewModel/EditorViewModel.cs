using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DPA_Musicsheets.Command;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Memento;
using DPA_Musicsheets.Util;

namespace DPA_Musicsheets.ViewModel
{
    public class EditorViewModel : BaseViewModel
    {
        public MementoViewModel SlotOne { get; set; }

        public MementoViewModel SlotTwo { get; set; }

        private readonly EditorMemento _editorMemento;

        public string Editor
        {
            get { return _editorMemento.Content; }
            // No need to raise propertychanged-event
            // Already done in the EditorMemento-class
            set { _editorMemento.Content = value; }
        }

        private readonly IPluginWriter<string> _lilypondPluginWriter;

        private const double GeneratorDelay = 1.5D;

        private int _editorHash;

        private readonly DelayedActionHandler _delayedActionHandler;

        public EditorViewModel(IPluginWriter<string> lilypondPluginWriter)
            : this()
        {
            _lilypondPluginWriter = lilypondPluginWriter;
        }

        public EditorViewModel()
        {
            SlotOne = new MementoViewModel(this);
            SlotTwo = new MementoViewModel(this);
            _editorMemento = new EditorMemento(string.Empty);
            _delayedActionHandler = new DelayedActionHandler(GeneratorDelay);

            _editorMemento.PropertyChanged += (sender, args) => // Model = INotifyPropertyChanged
            {
                RaisePropertyChanged(nameof(Editor));
            };
            PropertyChanged += (sender, args) => // ViewModel = INotifyPropertyChanged
            {
                if (args.PropertyName.Equals(nameof(Editor)))
                    OnEditorChange();
            };
        }

        private void OnEditorChange()
        {
            _delayedActionHandler.RunAsync(async () =>
            {
                if (_editorHash == 0)
                {
                    await LilyPondGeneratorAsync();
                }
            });
        }

        private async Task LilyPondGeneratorAsync()
        {
            _editorHash = Editor.GetHashCode();
            try
            {
                var sheet = _lilypondPluginWriter?.WriteSheet(Editor);
                // todo: sheet to visual note bar
                var newGenHashCode = Editor.GetHashCode();
                if (newGenHashCode != _editorHash) // regenerate, because user is a fuckwit and changes shit.
                {
                    await LilyPondGeneratorAsync();
                }
                else
                {
                    _editorHash = 0;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }   
        }

        public class MementoViewModel : BaseViewModel, IClonable<MementoViewModel>
        {
            private readonly EditorViewModel _parent;

            private EditorMemento _memento;

            public ICommand Save { get; set; }

            public ICommand Load { get; set; }

            public MementoViewModel(EditorViewModel parent)
            {
                _parent = parent;
                Save = new RelayCommand(OnSave);
                Load = new RelayCommand(OnLoad, CanLoad);
            }

            private void OnSave(object args) => _memento = _parent._editorMemento.Clone();

            private void OnLoad(object args) => _parent._editorMemento.Restore(_memento);

            private bool CanLoad(object args) => _memento != null;

            public MementoViewModel Clone()
            {
                return new MementoViewModel(_parent);
            }
        }
    }
}
