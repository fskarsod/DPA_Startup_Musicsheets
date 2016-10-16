using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Model;
using DPA_Musicsheets.Core.Model.Enum;
using PSAMControlLibrary;
using Note = DPA_Musicsheets.Core.Model.Note;
using Rest = DPA_Musicsheets.Core.Model.Rest;
using PsamRest = PSAMControlLibrary.Rest;

namespace DPA_Musicsheets.VisualNotes
{
    public interface IVisualNoteVisitor : IMusicComponentVisitor
    {
        MusicalSymbol Result { get; }
    }

    public class VisualNoteVisitor : IVisualNoteVisitor
    {
        private readonly IDictionary<Accidental, int> _accidentalDictionary;

        private readonly IDictionary<double, MusicalSymbolDuration> _symbolDurationDictionary;

        public MusicalSymbol Result { get; private set; }

        public VisualNoteVisitor()
        {
            _accidentalDictionary = new Dictionary<Accidental, int>();
            _symbolDurationDictionary = new Dictionary<double, MusicalSymbolDuration>();
            InitializeDictionaries();
        }

        private void InitializeDictionaries()
        {
            _accidentalDictionary.Add(Accidental.Sharp, 1);
            _accidentalDictionary.Add(Accidental.Flat, -1);
            var length = 1D;
            foreach (var symbolDuration in Enum.GetValues(typeof(MusicalSymbolDuration)).Cast<MusicalSymbolDuration>())
            {
                _symbolDurationDictionary.Add(length, symbolDuration);
                _symbolDurationDictionary.Add(length * 1.5D, symbolDuration);
                length /= 2;
            }
        }

        public void Visit(IMusicComponent musicComponent)
        { }

        public void Visit(BarBoundary barBoundary)
        {
            Result = new Barline();
        }

        public void Visit(BaseNote baseNote)
        {
            Result = new PsamRest(_symbolDurationDictionary[baseNote.LengthValue]) { NumberOfDots = baseNote.HasDot ? 1 : 0 };
        }

        public void Visit(Note note)
        {
            Result = new PSAMControlLibrary.Note(
                note.Pitch.ToString(),
                ConvertAccident(note.Accidental),
                note.Octave,
                _symbolDurationDictionary[note.LengthValue],
                NoteStemDirection.Down,
                NoteTieType.None,
                new List<NoteBeamType> { NoteBeamType.Single })
            { NumberOfDots = note.HasDot ? 1 : 0, };
        }

        private int ConvertAccident(Accidental accidental)
        {
            return _accidentalDictionary.ContainsKey(accidental)
                ? _accidentalDictionary[accidental]
                : 0;
        }
    }
}
