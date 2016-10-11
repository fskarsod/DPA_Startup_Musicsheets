using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup.Localizer;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Model;
using DPA_Musicsheets.Core.Model.Enum;
using PSAMControlLibrary;
using Note = DPA_Musicsheets.Core.Model.Note;
using Rest = PSAMControlLibrary.Rest;

namespace DPA_Musicsheets.VisualNotes
{
    public class VisualNoteReaderPlugin : IMusicComponentVisitor, IPluginReader<IEnumerable<MusicalSymbol>>
    {
        public IEnumerable<MusicalSymbol> ReadSheet(Sheet sheet)
        {
            foreach (var provider in sheet.Tracks[1].MusicComponentProviders)
            {
                foreach (var component in provider.GetMusicComponents())
                {
                    component.Accept(this);
                    yield return Last;
                }
            }
        }

        public MusicalSymbol Last { get; set; }

        public VisualNoteReaderPlugin()
        {
            InitDictionary();
        }

        public void Visit(IMusicComponent musicComponent)
        {
            throw new NotImplementedException();
        }

        public void Visit(BarBoundary barBoundary)
        {
            Last = new Barline();
        }

        private IDictionary<double, MusicalSymbolDuration> _test;

        private void InitDictionary()
        {
            _test = new Dictionary<double, MusicalSymbolDuration>();
            var length = 1D;
            foreach (var symbolDuration in Enum.GetValues(typeof(MusicalSymbolDuration)).Cast<MusicalSymbolDuration>())
            {
                _test.Add(length, symbolDuration);
                _test.Add(length * 1.5D, symbolDuration);
                length /= 2;
            }
        }

        public void Visit(BaseNote baseNote)
        {
            Last = new Rest(_test[baseNote.LengthValue]) { NumberOfDots = baseNote.HasDot ? 1 : 0 };
        }

        private static IDictionary<Accidental, int> AccidentalDictionary => new Dictionary<Accidental, int>
        {
            { Accidental.Sharp, 1 },
            { Accidental.Flat, -1 }
        };

        private static int ConvertAccident(Accidental accidental)
        {
            return AccidentalDictionary.ContainsKey(accidental)
                ? AccidentalDictionary[accidental]
                : 0;
        }

        public void Visit(Note note)
        {
            // string noteStep, int noteAlter, int noteOctave,
            // MusicalSymbolDuration noteDuration, NoteStemDirection noteStemDirection,
            // NoteTieType noteTieType, List<NoteBeamType> noteBeamList
            Last = new PSAMControlLibrary.Note(
                note.Pitch.ToString(),
                ConvertAccident(note.Accidental),
                note.Octave,
                _test[note.LengthValue],
                NoteStemDirection.Down,
                NoteTieType.None,
                new List<NoteBeamType> { NoteBeamType.Single })
            { NumberOfDots = note.HasDot ? 1 : 0, };
        }
    }
}
