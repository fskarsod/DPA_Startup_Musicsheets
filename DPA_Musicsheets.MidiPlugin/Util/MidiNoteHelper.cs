using DPA_Musicsheets.Core.Builder.Interface;
using DPA_Musicsheets.Core.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.MidiPlugin.Util
{
    public static class MidiNoteHelper
    {
        // Octave system: https://andymurkin.files.wordpress.com/2012/01/midi-int-midi-note-no-chart.jpg
        private static readonly Pitch[] NotePitchSequence =
        {
            Pitch.C, Pitch.C, Pitch.D, Pitch.D, Pitch.E, Pitch.F, Pitch.F, Pitch.G, Pitch.G, Pitch.A, Pitch.A, Pitch.B
        };

        // Octave system: https://andymurkin.files.wordpress.com/2012/01/midi-int-midi-note-no-chart.jpg
        private static readonly Accidental[] NoteAccidentalSequence =
        {
            Accidental.Natural, Accidental.Sharp, Accidental.Natural, Accidental.Sharp, Accidental.Natural, Accidental.Natural, Accidental.Sharp, Accidental.Natural, Accidental.Sharp, Accidental.Natural, Accidental.Sharp, Accidental.Natural
        };

        private static readonly IDictionary<double, int[]> MusicComponentLengthDictionary = new Dictionary<double, int[]>
        {
            { 1d/1d,    new [] { 1, 1 } }, // full note
            { 3d/2d,    new [] { 3, 2 } }, // full note + dot

            { 1d/2d,    new [] { 1, 2 } }, // half note
            { 3d/4d,    new [] { 3, 4 } }, // half note + dot

            { 1d/4d,    new [] { 1, 4 } }, // fourth note
            { 3d/8d,    new [] { 3, 8 } }, // fourth note + dot

            { 1d/8d,    new [] { 1, 8  } }, // eighth note
            { 3d/16d,   new [] { 3, 16 } }, // eighth note + dot

            { 1d/16d,   new [] { 1, 16 } }, // sixteenth note
            { 3d/32d,   new [] { 3, 32 } }, // sixteenth note + dot

            { 1d/32d,   new [] { 1, 32 } }, // thirty second note
            { 3d/64d,   new [] { 3, 64 } }, // thirty second note + dot

            { 1d/64d,   new [] { 1, 64  } }, // fourthy sixth note
            { 3d/128d,  new [] { 3, 128 } }  // fourthy sixth note + dot
        };

        public static void NoteLengthConverter<T>(IMusicComponentLengthBuilder<T> builder, double noteLength = 0)
            where T : IMusicComponentLengthBuilder<T>
        {
            var noteValues = MusicComponentLengthDictionary.ContainsKey(noteLength)
                ? MusicComponentLengthDictionary[noteLength]
                : MusicComponentLengthDictionary[1d/1d];
            var dur = noteValues[1]/(noteValues[0] == 3 ? 2 : 1);
            var dot = noteValues[0] == 3;
            builder.SetDuration(dur)
                .HasDot(dot);
        }

        /// <summary>
        ///     Converts keycode to a note type.
        ///     Consists of type, octave and modifier.
        /// </summary>
        public static void NoteTypeConverter(INoteBuilder builder, int keycode)
        {
            var noteIndex = keycode % 12;
            var octave = (ushort)Math.Floor((double)keycode / 12);
            builder.SetOctave(--octave)
                .SetAccidental(NoteAccidentalSequence[noteIndex])
                .SetPitch(NotePitchSequence[noteIndex]);
        }
    }
}
