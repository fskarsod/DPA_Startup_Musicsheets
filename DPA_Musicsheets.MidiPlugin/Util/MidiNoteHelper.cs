using System;
using System.Linq;
using System.Collections.Generic;
using DPA_Musicsheets.Core.Model;
using Type = DPA_Musicsheets.Core.Model.Type;

namespace DPA_Musicsheets.MidiPlugin.Util
{
    public static class MidiNoteHelper
    {
        // Octave system: https://andymurkin.files.wordpress.com/2012/01/midi-int-midi-note-no-chart.jpg
        private readonly static Type[] NoteTypeSequence =
        {
            Type.C, Type.C, Type.D, Type.D, Type.E, Type.F, Type.F, Type.G, Type.G, Type.A, Type.A, Type.B
        };

        // Octave system: https://andymurkin.files.wordpress.com/2012/01/midi-int-midi-note-no-chart.jpg
        private readonly static Modifier[] NoteModifierSequence =
        {
            Modifier.Natural, Modifier.Sharp, Modifier.Natural, Modifier.Sharp, Modifier.Natural, Modifier.Natural, Modifier.Sharp, Modifier.Natural, Modifier.Sharp, Modifier.Natural, Modifier.Sharp, Modifier.Natural
        };

        private readonly static IDictionary<double, Fraction> NoteLengthTable = new Dictionary<double, Fraction>
        {
            { 1d/1d,    new Fraction(1, 1)  }, // full note
            { 3d/2d,    new Fraction(3, 2)  }, // full note + dot

            { 1d/2d,    new Fraction(1, 2)  }, // half note
            { 3d/4d,    new Fraction(3, 4)  }, // half note + dot

            { 1d/4d,    new Fraction(1, 4)  }, // fourth note
            { 3d/8d,    new Fraction(3, 8)  }, // fourth note + dot

            { 1d/8d,    new Fraction(1, 8)  }, // eighth note
            { 3d/16d,   new Fraction(3, 16) }, // eighth note + dot

            { 1d/16d,   new Fraction(1, 16) }, // sixteenth note
            { 3d/32d,   new Fraction(3, 32) }, // sixteenth note + dot

            { 1d/32d,   new Fraction(1, 32) }, // thirty second note
            { 3d/64d,   new Fraction(3, 64) }, // thirty second note + dot

            { 1d/64d,   new Fraction(1, 64) }, // fourthy sixth note
            { 3d/128d,  new Fraction(3, 128) } // fourthy sixth note + dot
        };

        /// <summary>
        ///     Converts keycode to a note type.
        ///     Consists of type, octave and modifier.
        /// </summary>
        public static Note NoteTypeConverter(int keycode)
        {
            var noteIndex = keycode % 12;
            var octave = (ushort)Math.Floor((double)keycode / 12);
            return new Note
            {
                Octave = --octave,
                Modifier = NoteModifierSequence[noteIndex],
                Type = NoteTypeSequence[noteIndex]
            };
        }

        /// <summary>
        ///     Converts keycode to a note type.
        ///     Consists of type, octave and modifier.
        /// </summary>
        public static int NoteTypeConverter(Note note)
        {
            var type = Type.G;
            var modifier = Modifier.Natural;
            while (type = NoteTypeSequence.GetEnumerator() && modifier = NoteModifierSequence.GetEnumerator())
            {
                string x = type.ToString();
                var y = modifier.ToString();
            }


            // var octave = (ushort)Math.Floor((double)keycode / 12);

            //var noteIndex = keycode % 12;
            //var octave = (ushort)Math.Floor((double)keycode / 12);
            //return new Note
            //{
            //    Octave = --octave,
            //    Modifier = NoteModifierSequence[noteIndex],
            //    Type = NoteTypeSequence[noteIndex]
            //};
        }

        public static Fraction NoteLengthConverter(double noteLength = 0)
        {
            return NoteLengthTable.ContainsKey(noteLength)
                ? NoteLengthTable[noteLength]
                : NoteLengthTable[1d / 1d];
        }
    }
}
