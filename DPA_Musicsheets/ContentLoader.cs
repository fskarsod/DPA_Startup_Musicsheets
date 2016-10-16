using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Model;
using DPA_Musicsheets.VisualNotes;
using PSAMControlLibrary;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets
{
    public interface IContentLoader : IVisualNoteLoader
    {
        void FromLilypond();

        void FromMidi();
    }
    
    public class ContentLoader : IContentLoader
    {
        private readonly IApplicationContext _applicationContext;
        private readonly IPluginReader<IEnumerable<MusicalSymbol>> _visualNotsePluginReader;
        private readonly IPluginReader<string> _lilypondPluginReader;
        private readonly IPluginWriter<string> _lilypondPluginWriter;
        private readonly IPluginWriter<Sequence> _midiPluginWriter;

        public event LoadVisualNotes LoadVisualNotes;

        public ContentLoader(IApplicationContext applicationContext,
            IPluginReader<IEnumerable<MusicalSymbol>> visualNotsePluginReader,
            IPluginReader<string> lilypondPluginReader, 
            IPluginWriter<string> lilypondPluginWriter,
            IPluginWriter<Sequence> midiPluginWriter)
        {
            _applicationContext = applicationContext;
            _lilypondPluginWriter = lilypondPluginWriter;
            _midiPluginWriter = midiPluginWriter;
            _lilypondPluginReader = lilypondPluginReader;
            _visualNotsePluginReader = visualNotsePluginReader;
        }

        public void FromLilypond()
        {
            var text = File.ReadAllText(_applicationContext.FileLocation);
            //var sheet = _lilypondPluginWriter.WriteSheet(text); // todo: lilypond enable
            //LoadToVisualNotes(sheet);
            _applicationContext.EditorMemento.Content = text;
        }

        public void FromMidi()
        {
            var sequence = new Sequence();
            sequence.Load(_applicationContext.FileLocation);
            var sheet = _midiPluginWriter.WriteSheet(sequence);
            LoadToVisualNotes(sheet);
            var lilypond = _lilypondPluginReader.ReadSheet(sheet);
            _applicationContext.EditorMemento.Content = lilypond;
        }

        private void LoadToVisualNotes(Sheet sheet)
        {
            var symbols = _visualNotsePluginReader.ReadSheet(sheet);
            LoadVisualNotes?.Invoke(symbols);
        }
    }
}
