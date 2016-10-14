using System;
using Microsoft.Win32;
using PSAMControlLibrary;
using Sanford.Multimedia.Midi;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.MidiPlugin.Plugin;
using DPA_Musicsheets.VisualNotes;
using PSAMWPFControlLibrary;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DPA_Musicsheets.Command;
using DPA_Musicsheets.Util;
using DPA_Musicsheets.ViewModel;

namespace DPA_Musicsheets
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CancelExit = new RelayCommand(OnCancelExit);
        }

        #region VISUALNOTES
        IIncipitViewer _currentStaff = null;

        private void UpdateStaff()
        {
            if (LimitReached(_currentStaff))
            {
                NewStaff();
            }
        }

        private void NewStaff()
        {
            var viewer = new IncipitViewerWPF
            {
                Width = 525D,
                VerticalAlignment = VerticalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            _currentStaff = viewer;
            _currentStaff.AddMusicalSymbol(new Clef(ClefType.GClef, 2));
            _currentStaff.AddMusicalSymbol(new TimeSignature(TimeSignatureType.Numbers, 4, 4));
            StaffViewerPanel.Children.Add(viewer);
        }

        private static bool LimitReached(IIncipitViewer viewer)
        {
            return viewer.CountMusicalSymbols() >= 20;
        }

        private void AddSymbolToStaff(MusicalSymbol symbol)
        {
            _currentStaff.AddMusicalSymbol(symbol);
            UpdateStaff();
        }
		
        // move to OnShowContent in viewmodel
		private void OnShowContent()
		{
			NewStaff();
			var sequence = new Sequence();
            sequence.Load(string.Empty); // todo: fix this
            var writer = new MidiPluginWriter();
            var sheet = writer.WriteSheet(sequence);
            var reader = new VisualNoteReaderPlugin();
            var result = reader.ReadSheet(sheet);
            foreach (var symbol in result)
            {
                AddSymbolToStaff(symbol);
            }
		}
        #endregion
        
        #region SAVE BEFORE CLOSE
        private bool _saved = false; // todo: link to save-sequence

        private IDictionary<MessageBoxResult, ICommand> WindowClosingDictionary => new Dictionary<MessageBoxResult, ICommand>
        {
            { MessageBoxResult.No, new RelayCommand(OnSaveBeforeExit) },
            { MessageBoxResult.Yes, new RelayCommand(OnForceExit) }
        };

        private ICommand CancelExit { get; set; }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!_saved)
            {
                BeforeExitSequence(e);
            }
            else
            {
                CloseApplication();
            }
        }

        private void BeforeExitSequence(CancelEventArgs e)
        {
            var result = MessageBox.Show("Do you want to exit without saving", "EXIT", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if (WindowClosingDictionary.ContainsKey(result))
            {
                WindowClosingDictionary[result].Execute(e);
            }
            else
            {
                CancelExit.Execute(e);
            }
        }

        // save-sequence
        private void OnSaveBeforeExit(object parameters)
        {
            var saveFileDialog = new SaveFileDialog { Filter = "Midi Files(.mid)|*.mid" };
            if (saveFileDialog.ShowDialog() == true)
            {
                var file = saveFileDialog.FileName;
                File.WriteAllText(file, "andansldbaskjdnjkasdjkadkjabdkhbakd"); // todo: get EditorText
                _saved = true;
                CloseApplication();
            }
            else // cancel save dialog -> no exit
            {
                CancelExit.Execute(parameters);
            }
        }

        private void OnForceExit(object parameters)
        {
            CloseApplication();
        }

        private void OnCancelExit(object parameters)
        {
            var cancelEventArgs = parameters as CancelEventArgs;
            if (cancelEventArgs != null)
                cancelEventArgs.Cancel = true;
        }

        #endregion

        private void CloseApplication()
        {
            (DataContext as MainWindowViewModel)?.Dispose();
        }

        private void Window_OnKeyDown(object sender, KeyEventArgs e)
        {
            (DataContext as MainWindowViewModel)?.ShortcutHandler?.AddKey(e.Key);
            // throw new NotImplementedException();
        }

        private void Window_OnKeyUp(object sender, KeyEventArgs e)
        {
            (DataContext as MainWindowViewModel)?.ShortcutHandler?.RemoveKey(e.Key);
            // throw new NotImplementedException();
        }
    }
}
