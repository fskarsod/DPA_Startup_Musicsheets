using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DPA_Musicsheets.Command;
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

        #region LILYPOND GENERATION FIELDS
        private const double GeneratorDelay = 1.5D;

        private int _generatorHashCode;

        private readonly DelayedActionHandler _delayedActionHandler;
        #endregion

        public EditorViewModel()
        {
            SlotOne = new MementoViewModel(this);
            SlotTwo = new MementoViewModel(this);
            _editorMemento = new EditorMemento(string.Empty);
            _delayedActionHandler = new DelayedActionHandler(GeneratorDelay);

            _editorMemento.PropertyChanged += (sender, args) =>
            {
                RaisePropertyChanged(nameof(Editor));
            };
            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName.Equals(nameof(Editor)))
                    OnEditorChange();
            };
        }

        #region LILYPOND GENERATION Methods
        private void OnEditorChange()
        {
            _delayedActionHandler.Run(() =>
            {
                if (_generatorHashCode != 0)
                {
                    // this is View-shit
                    MessageBox.Show("1.5 second have passed, but shit is generating.");
                }
                else
                {
                    // this is View-shit
                    MessageBox.Show("1.5 second have passed, app is starting generation.");
                    LilyPondGenerator();
                }
            });
        }

        private void LilyPondGenerator()
        {
            _generatorHashCode = Editor.GetHashCode();
            new DelayedActionHandler(3d).Run(() => // Replace this line with the LilyPond-Generation code.
            {
                var newGenHashCode = Editor.GetHashCode();
                if (newGenHashCode != _generatorHashCode) // regenerate, because user is a fuckwit and changes shit.
                {
                    LilyPondGenerator();
                }
                else
                {
                    _generatorHashCode = 0;
                }
            });
        }
        #endregion

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
