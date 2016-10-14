using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DPA_Musicsheets.Command;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Util;
using DPA_Musicsheets.VisualNotes;

namespace DPA_Musicsheets.ViewModel
{
    public class EditorViewModel : BaseViewModel
    {
        private const double GeneratorDelay = 1.5D;

        private readonly IMemento<EditorMemento> _editorMemento;

        private readonly IPluginWriter<string> _lilypondPluginWriter;

        private readonly DelayedActionHandler _delayedActionHandler;

        private IMusicalSymbolConsumer _musicalSymbolConsumer;

        #region public string Editor { get; set; } // _editorMemento.Context.Content;
        public string Editor
        {
            get { return _editorMemento.Context.Content; }
            // No need to raise propertychanged-event
            // Already done in the EditorMemento-class
            set { _editorMemento.Context.Content = value; }
        }
        #endregion

        private int _editorHash;
        
        public MementoViewModel SlotOne { get; set; }

        public MementoViewModel SlotTwo { get; set; }
        
        public EditorViewModel(IMemento<EditorMemento> editorMemento, IPluginWriter<string> lilypondPluginWriter)
            : this()
        {
            _editorMemento = editorMemento;
            _lilypondPluginWriter = lilypondPluginWriter;

            _editorMemento.PropertyChanged += (sender, args) => // Model = INotifyPropertyChanged
            {
                RaisePropertyChanged(nameof(Editor));
            };
        }

        private EditorViewModel()
        {
            SlotOne = new MementoViewModel(this);
            SlotTwo = new MementoViewModel(this);
            _delayedActionHandler = new DelayedActionHandler(GeneratorDelay);
            
            PropertyChanged += (sender, args) => // ViewModel = INotifyPropertyChanged
            {
                if (args.PropertyName.Equals(nameof(Editor)))
                    OnEditorChange();
            };
        }

        public void SetMusicalSymbolConsumer(IMusicalSymbolConsumer musicalSymbolConsumer)
        {
            _musicalSymbolConsumer = musicalSymbolConsumer;
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
                // _musicalSymbolConsumer.Consume(null);
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
