using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using DPA_Musicsheets.Util;
using Microsoft.Win32;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private MidiButtonSetVieWModel _midiButtonSetVieWModel;

        public MidiButtonSetVieWModel MidiButtonSetVieWModel
        {
            get
            {
                return _midiButtonSetVieWModel;
            }
            set
            {
                _midiButtonSetVieWModel = value;
                RaisePropertyChanged("MidibuttonSetViewModel");
            }
        }

        public ICommand Test { get; set; }

        #region LILYPOND GENERATION FIELDS
        private const double GeneratorDelay = 1.5D;

        private int _generatorHashCode;

        private readonly DelayedActionHandler _delayedActionHandler;

        private string _editor;
        public string Editor
        {
            get { return _editor; }
            set
            {
                _editor = value;
                OnEditorChange();
            }
        }
        #endregion

        public MainWindowViewModel()
        {
            MidiButtonSetVieWModel = new MidiButtonSetVieWModel();

            _delayedActionHandler = new DelayedActionHandler(GeneratorDelay);

            Test = new RelayCommand(args => MessageBox.Show("FUCKYOU"), arg => Editor?.Length > 0);   
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

        public override void Dispose()
        {
            MidiButtonSetVieWModel.Dispose();
        }
    }
}
