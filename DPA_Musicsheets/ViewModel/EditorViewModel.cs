using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DPA_Musicsheets.Command;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Util;
using DPA_Musicsheets.VisualNotes;
using PSAMControlLibrary;

namespace DPA_Musicsheets.ViewModel
{
    public class EditorViewModel : BaseViewModel
    {
        private const double GeneratorDelay = 1.5D;

        private readonly IApplicationContext _applicationContext;

        private readonly IPluginWriter<string> _lilypondPluginWriter;
        private readonly IPluginReader<IEnumerable<MusicalSymbol>> _visualNotePluginReader;

        private readonly DelayedActionHandler _delayedActionHandler;

        private IMusicalSymbolConsumer _musicalSymbolConsumer;

        #region public string Content { get; set; } // _applicationContext.EditorMemento.Content;
        public string Content
        {
            get { return _applicationContext.EditorMemento.Content; }
            set { _applicationContext.EditorMemento.Content = value; }
        }
        #endregion

        private int _editorHash;
        
        public MementoViewModel SlotOne { get; set; }

        public MementoViewModel SlotTwo { get; set; }
        
        public EditorViewModel(IApplicationContext applicationContext,
            IPluginWriter<string> lilypondPluginWriter,
            IPluginReader<IEnumerable<MusicalSymbol>> visualNotePluginReader)
            : this()
        {
            _applicationContext = applicationContext;
            _lilypondPluginWriter = lilypondPluginWriter;
            _visualNotePluginReader = visualNotePluginReader;

            _applicationContext.EditorMemento.PropertyChanged += (sender, evt) => // Model = INotifyPropertyChanged
            {
                if (evt.PropertyName.Equals(nameof(EditorMemento.Content)))
                {
                    OnPropertyChanged(nameof(EditorMemento.Content));
                    OnEditorChange();
                }
            };
        }

        private EditorViewModel()
        {
            SlotOne = new MementoViewModel(this);
            SlotTwo = new MementoViewModel(this);
            _delayedActionHandler = new DelayedActionHandler(GeneratorDelay);
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
            _editorHash = Content.GetHashCode();
            try
            {
                var sheet = _lilypondPluginWriter.WriteSheet(Content);
                var symbols = _visualNotePluginReader.ReadSheet(sheet);
                _musicalSymbolConsumer.Consume(symbols);

                var newGenHashCode = Content.GetHashCode();
                if (newGenHashCode != _editorHash)
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

        public override void Dispose()
        {
            base.Dispose();
            _delayedActionHandler.Dispose();
        }

        public class MementoViewModel : BaseViewModel
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

            private void OnSave(object args) => _memento = _parent._applicationContext.EditorMemento.Clone();

            private void OnLoad(object args) => _parent._applicationContext.EditorMemento.Restore(_memento);

            private bool CanLoad(object args) => _memento != null;
        }
    }
}
