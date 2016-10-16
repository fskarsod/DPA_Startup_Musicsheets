using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DPA_Musicsheets.Core.Model;
using DPA_Musicsheets.LilypondPlugin.Plugin;
using DPA_Musicsheets.MidiPlugin.Util;
using Note = PSAMControlLibrary.Note;
using TimeSignature = PSAMControlLibrary.TimeSignature;
using DPA_Musicsheets.Command;
using DPA_Musicsheets.Util;
using DPA_Musicsheets.ViewModel;
using DPA_Musicsheets.VisualNotes;
using PSAMControlLibrary;
using PSAMWPFControlLibrary;

namespace DPA_Musicsheets
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMusicalSymbolConsumer
    {
        private const int SymbolPerIncipitviewer = 20;

        public MainWindowViewModel ViewModel => DataContext as MainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            ViewModel?.SetMusicalSymbolConsumer(this);
            ViewModel?.MidiButtonSetVieWModel?.SetMusicalSymbolConsumer(this);
            ViewModel?.EditorViewModel?.SetMusicalSymbolConsumer(this);
        }

        #region WINDOW EVENTS

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ViewModel?.WindowClosing?.Execute(e);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ViewModel?.Dispose();
        }

        private void Window_OnKeyDown(object sender, KeyEventArgs e)
        {
            ViewModel?.ShortcutHandler?.AddKey(e.Key);
            // var saveFileDialog = new SaveFileDialog { Filter = "Sheetmusic Files(*.mid;*.ly)|*.mid;*.ly|Midi Files(.mid)|*.mid|Lilypond Files(.ly)|*.ly" };
            // if (saveFileDialog.ShowDialog() == true)
            // {
                // var file = saveFileDialog.FileName;
                // File.WriteAllText(file, "andansldbaskjdnjkasdjkadkjabdkhbakd"); // todo: get EditorText
                // _saved = true;
                // CloseApplication();
            // }
            // else // cancel save dialog -> no exit
            // {
                // CancelExit.Execute(parameters);
            // }
        }

        private void Window_OnKeyUp(object sender, KeyEventArgs e)
        {
            ViewModel?.ShortcutHandler?.RemoveKey(e.Key);
        }

        #endregion

        #region PSAM INCIPITVIEWER VIEW STUFF
        private IIncipitViewer _currentStaff = null;

        public void Consume(IEnumerable<MusicalSymbol> symbols)
        {
            NewStaff();
            foreach (var symbol in symbols)
            {
                AddSymbolToStaff(symbol);
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

        private void AddSymbolToStaff(MusicalSymbol symbol)
        {
            _currentStaff.AddMusicalSymbol(symbol);
            UpdateStaff();
        }

        private void UpdateStaff()
        {
            if (LimitReached(_currentStaff))
            {
                NewStaff();
            }
        }

        private static bool LimitReached(IIncipitViewer viewer)
        {
            return viewer.CountMusicalSymbols() >= SymbolPerIncipitviewer;
        }

        #endregion
    }
}
