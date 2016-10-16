using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DPA_Musicsheets;
using DPA_Musicsheets.Command;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.IoC;
using DPA_Musicsheets.MidiControl;
using DPA_Musicsheets.MidiPlugin.Plugin;
using DPA_Musicsheets.Shortcut;
using DPA_Musicsheets.Util;
using DPA_Musicsheets.ViewModel;
using DPA_Musicsheets.VisualNotes;
using PSAMControlLibrary;
using Sanford.Multimedia.Midi;
using Key = System.Windows.Input.Key;

namespace DPA_Musicsheets
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            RegisterAll(IoCContainer.Instance);
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            IoCContainer.Instance.Dispose();
        }

        private static void RegisterAll(IContainer container)
        {
            // Plugins
            container.RegisterTransient<IVisualNoteVisitor>(c => new VisualNoteVisitor());
            container.RegisterTransient<IPluginReader<IEnumerable<MusicalSymbol>>>(c => new VisualNoteReaderPlugin(c.Resolve<IVisualNoteVisitor>()));

            container.RegisterTransient<IPluginWriter<Sequence>>(c => new MidiPluginWriter());

            // Initial stuff
            container.RegisterSingleton(new OutputDevice(0));
            container.RegisterTransient<IDialogService>(c => new DialogService());
            container.RegisterSingleton<IApplicationContext>(new ApplicationContext());
            container.RegisterSingleton<IContentLoader>(c => new ContentLoader(c.Resolve<IApplicationContext>(), c.Resolve<IPluginReader<IEnumerable<MusicalSymbol>>>(), c.Resolve<IPluginReader<string>>(), c.Resolve<IPluginWriter<string>>(), c.Resolve<IPluginWriter<Sequence>>()));
            container.RegisterTransient<IContentStorage>(c => new ContentStorage(c.Resolve<IApplicationContext>(), c.Resolve<IContentLoader>(), c.Resolve<IDialogService>()));
            container.RegisterSingleton<IMidiPlayerControl>(c => new MidiPlayerControl(c.Resolve<OutputDevice>(), c.Resolve<IApplicationContext>(), c.Resolve<IDialogService>()), true);

            // Commands
            container.RegisterTransient<IStopCommand>(c => new StopCommand(c.Resolve<IMidiPlayerControl>()));
            container.RegisterTransient<IPlayCommand>(c => new PlayCommand(c.Resolve<IMidiPlayerControl>()));
            container.RegisterTransient<IOpenFileCommand>(c => new OpenFileCommand(c.Resolve<IContentStorage>()));
            container.RegisterTransient<IInsertCommand>(c => new InsertCommand(c.Resolve<IApplicationContext>()));
            container.RegisterTransient<ICancelCommand>(c => new CancelCommand());
            container.RegisterTransient<ISaveFileCommand>(c => new SaveFileCommand(c.Resolve<IContentStorage>(), c.Resolve<ICancelCommand>()));
            container.RegisterTransient<IWindowClosingCommand>(c => new WindowClosingCommand(c.Resolve<IContentStorage>(), c.Resolve<IDialogService>(), c.Resolve<ISaveFileCommand>()));

            // Shortcut control
            container.RegisterTransient(c => new ShortcutChain(c));
            container.RegisterTransient(c => new ShortcutHandler(KeyShortcutDictionary, c.Resolve<ShortcutChain>()));

            // Concrete shortcuts
            container.RegisterTransient(c => new SaveFileShortcut(c.Resolve<ISaveFileCommand>()));
            container.RegisterTransient(c => new SavePdfShortcut(c.Resolve<ISaveFileCommand>()));
            container.RegisterTransient(c => new OpenFileShortcut(c.Resolve<IOpenFileCommand>()));
            container.RegisterTransient(c => new PlayMidiShortcut(c.Resolve<IPlayCommand>()));
            container.RegisterTransient(c => new InsertTrebleShortcut(c.Resolve<IInsertCommand>()));
            container.RegisterTransient(c => new InsertTempoShortcut(c.Resolve<IInsertCommand>()));
            container.RegisterTransient(c => new InsertDefaultTimeSigShortcut(c.Resolve<IInsertCommand>()));
            container.RegisterTransient(c => new InsertFourFourTimeSigShortcut(c.Resolve<IInsertCommand>()));
            container.RegisterTransient(c => new InsertThreeFourTimeSigShortcut(c.Resolve<IInsertCommand>()));
            container.RegisterTransient(c => new InsertSixEightTimeSigShortcut(c.Resolve<IInsertCommand>()));

            // Viewmodels
            container.RegisterTransient(c => new EditorViewModel(c.Resolve<IApplicationContext>(), null, c.Resolve<IPluginReader<IEnumerable<MusicalSymbol>>>()));
            container.RegisterTransient(c => new MidiButtonSetVieWModel(c.Resolve<IApplicationContext>(), c.Resolve<IPluginWriter<string>>(), c.Resolve<IPluginReader<IEnumerable<MusicalSymbol>>>(), c.Resolve<IPlayCommand>(), c.Resolve<IStopCommand>(), c.Resolve<IOpenFileCommand>(), c.Resolve<ISaveFileCommand>()));
            container.RegisterTransient(c => new MainWindowViewModel(c.Resolve<MidiButtonSetVieWModel>(), c.Resolve<EditorViewModel>(), c.Resolve<ShortcutHandler>(), c.Resolve<IContentLoader>(), c.Resolve<IWindowClosingCommand>()));
        }

        private static readonly IDictionary<IEnumerable<Key>, string> KeyShortcutDictionary = new Dictionary<IEnumerable<Key>, string>(new EnumerableKeyEqualityComparer())
        {
            { new [] { Key.LeftCtrl, Key.S }, "SaveFile" },
            { new [] { Key.LeftCtrl, Key.S, Key.P }, "SavePDF" },
            { new [] { Key.LeftCtrl, Key.O }, "OpenFile" },
            { new [] { Key.LeftCtrl, Key.M }, "PlayMidi" },
            { new [] { Key.LeftAlt, Key.C }, "InsertTreble" },
            { new [] { Key.LeftAlt, Key.S }, "InsertTempo120" },
            { new [] { Key.LeftCtrl, Key.T }, "InsertDefaultTimeSignature" },
            { new [] { Key.LeftCtrl, Key.T, Key.D4 }, "Insert4/4TimeSignature" },
            { new [] { Key.LeftCtrl, Key.T, Key.D3 }, "Insert3/4TimeSignature" },
            { new [] { Key.LeftCtrl, Key.T, Key.D6 }, "Insert6/8TimeSignature" }
        };
    }
}
