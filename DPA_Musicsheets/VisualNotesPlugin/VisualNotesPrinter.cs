using System;
using System.Collections.Generic;
using DPA_Musicsheets.Core.Interface;
using PSAMControlLibrary;

using IMusicFragment = DPA_Musicsheets.Core.Model.IMusicFragment;
using NoteModifier = DPA_Musicsheets.Core.Model.Modifier;
using NoteType = DPA_Musicsheets.Core.Model.Type;

using SheetModel = DPA_Musicsheets.Core.Model.Sheet;
using RestModel = DPA_Musicsheets.Core.Model.Rest;
using NoteModel = DPA_Musicsheets.Core.Model.Note;
using Fraction = DPA_Musicsheets.Core.Model.Fraction;

namespace DPA_Musicsheets.VisualNotesPlugin
{
    public class VisualNotesPrinter : ISheetPrinter<IEnumerable<MusicalSymbol>>
    {
        private Fraction _latestTimeSignature;
        private NoteModifier _lastestNoteModifier;

        public VisualNotesPrinter()
        {
            _lastestNoteModifier = NoteModifier.Natural;
        }

        public IEnumerable<MusicalSymbol> PrintSheet(SheetModel sheet)
        {
            yield return new Clef(ClefType.GClef, 2);
            _latestTimeSignature = null;

            //yield return new Clef(ClefType.GClef, 2);
            //yield return new TimeSignature(TimeSignatureType.Numbers, 4, 4);
            //yield return new Note("A", 0, 4, MusicalSymbolDuration.Sixteenth, NoteStemDirection.Down, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Start, NoteBeamType.Start });

            foreach (var fragment in sheet.Tracks[0].MusicFragments)
            {
                if (_latestTimeSignature == null || _latestTimeSignature != fragment.TimeSignature)
                {
                    _latestTimeSignature = fragment.TimeSignature;
                    yield return new TimeSignature(TimeSignatureType.Numbers, (uint)fragment.TimeSignature.Numerator, (uint)fragment.TimeSignature.Denominator);
                }
                yield return ConvertFragment(fragment);
            }
        }

        private MusicalSymbol ConvertFragment(IMusicFragment fragment)
        {
            var rest = fragment as RestModel;
            if (rest != null)
            { return new Rest(GetNoteDuration(rest.Length)); }

            var note = fragment as NoteModel;
            if (note == null)
            { return new Rest(MusicalSymbolDuration.Whole); }

            var dots = 0;
            if (note.Length.Numerator == 3)
            {
                dots = 1;
            }
            return new Note(note.Type.ToString(), GetModifierNumeral(note.Modifier), note.Octave, GetNoteDuration(note.Length), GetNoteStemDirection(note), NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Single })
            {
                NumberOfDots = dots
            };
        }

        private NoteStemDirection GetNoteStemDirection(NoteModel note)
        {
            /*
             * Depends on octave and note type in combination with Clef
             * E F G A B C D E -> D is turning point.
             */
            return NoteStemDirection.Up;
        }

        /// <summary>
        ///    -1: flat
        ///     0: no sign
        ///     1: sharp
        ///     2: natural
        /// </summary>
        private int GetModifierNumeral(NoteModifier modifier)
        {
            if (_lastestNoteModifier == modifier)
            { return 0; } // Same note modifier -> no sign

            switch (modifier) // Different modifier -> new sign
            {
                case NoteModifier.Flat:
                    _lastestNoteModifier = NoteModifier.Flat;
                    return -1;
                case NoteModifier.Sharp:
                    _lastestNoteModifier = NoteModifier.Sharp;
                    return 1;
                default:
                    _lastestNoteModifier = NoteModifier.Natural;
                    return 0;
            }
        }

        /// <summary>
        ///     Whole = 1,
        ///     Half = 2,
        ///     Quarter = 4,
        ///     Unknown = 6,
        ///     Eighth = 8,
        ///     Sixteenth = 16,
        ///     d32nd = 32,
        ///     d64th = 64,
        ///     d128th = 128,
        /// </summary>
        private MusicalSymbolDuration GetNoteDuration(Fraction length)
        {
            return length.Numerator == 1
                ? (MusicalSymbolDuration)(length.Denominator / length.Numerator)
                : (MusicalSymbolDuration)(length.Denominator / 2);
        }
    }
}