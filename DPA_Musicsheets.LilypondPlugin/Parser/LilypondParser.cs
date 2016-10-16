using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using DPA_Musicsheets.Core.Model;
using DPA_Musicsheets.Core.Model.Enum;
using DPA_Musicsheets.LilypondPlugin.Enum;

namespace DPA_Musicsheets.LilypondPlugin.Parser
{
    public static class LilypondParser
    {
        public static BaseNote CreateNoteFromString(string noteString, ref int relativeOctave, ref char relativeNote)
        {
            var p = LilypondParser.GetPitch(noteString.ToLower()[0]);

            if (p == null)
                throw new InvalidOperationException($"Cannot parse value \"{noteString.ToLower()[0]}\" to valid note. Please verify your source file.");

            if (p == Pitch.Rest)
            {
                var duration = LilypondParser.GetDuration(noteString, false);
                var hasDot = noteString[noteString.Length - 2] == '.';

                return new Rest
                {
                    Duration = duration,
                    HasDot = hasDot
                };
            }
            else
            {
                var pitch = (Pitch)p;
                var accidental = noteString.Length >= 4
                    ? LilypondParser.GetAccidental(noteString.Substring(1, 2))
                    : Accidental.Natural;
                var octaveOffset = LilypondParser.GetOctaveOffset(noteString, relativeNote);
                var duration = LilypondParser.GetDuration(noteString, (accidental != Accidental.Natural));
                var hasDot = noteString[noteString.Length - 1] == '.';

                relativeNote = noteString.ToLower()[0];
                relativeOctave = octaveOffset;

                return new Note
                {
                    Accidental = accidental,
                    Duration = duration,
                    HasDot = hasDot,
                    Octave = octaveOffset + relativeOctave,
                    Pitch = pitch
                };
            }
        }

        public static bool IsNote(string s)
        {
            return LilypondParser.PitchDictionary.ContainsKey(s[0]) && s.Length > 1;
        }

        public static TimeSignature GetTimeSignature(string s)
        {
            var sig = s.Split('/');
            return new TimeSignature(int.Parse(sig[0]), int.Parse(sig[1]));
        }

        public static LilypondKeyword? GetKeyword(string s)
        {
            LilypondKeyword rValue;

            return LilypondKeywordDictionary.TryGetValue(s.ToLower(), out rValue) ? (LilypondKeyword?) rValue : null;
        }

        public static int GetOctaveOffset(string s, char relativeNote)
        {
            var noteOffset = GetClosestNote(s[0], relativeNote);
            var octaveOffset = 0;

            if (s.Contains(','))
                octaveOffset = s.Count(c => c == ',') * -1;
            else if (s.Contains('\''))
                octaveOffset = s.Count(c => c == '\'');

            return octaveOffset + noteOffset;
        }

        private static int GetClosestNote(char note, char relativeNote)
        {
            // get the difference
            var diff = note - relativeNote;

            if (diff == 0)
                return 0;

            // flip the note if it's too far off
            if (diff > 3)
                diff -= 7;
            else if (diff < -3)
                diff += 7;

            // check what octave it belongs too (same, higher, lower)
            if (diff > 0)
                return 'g' - relativeNote >= diff ? 0 : 1;
            else if (diff < 0)
                return 'a' - relativeNote < diff ? 0 : -1;
            else
                return 0;
        }

        public static Clef? GetClef(string s)
        {
            Clef rValue;

            return ClefDictionary.TryGetValue(s.ToLower(), out rValue) ? (Clef?) rValue : null;
        }

        public static Tempo GetTempo(string s)
        {
            var splitString = s.Split('=');
            int noteDuration, bpm;

            if (int.TryParse(splitString[0], out noteDuration) && int.TryParse(splitString[1], out bpm))
                return new Tempo(noteDuration, bpm);
            else
                throw new InvalidOperationException($"Cannot parse value \"{s}\" to valid tempo. Please verify your source file.");
        }

        private static Pitch? GetPitch(char c)
        {
            Pitch rValue;

            return PitchDictionary.TryGetValue(c, out rValue) ? (Pitch?) rValue : null;
        }

        private static Accidental GetAccidental(string s)
        {
            return AccidentalDictionary.ContainsKey(s.ToLower()) ? AccidentalDictionary[s] : Accidental.Natural;
        }

        private static int GetDuration(string s, bool hasAccidental)
        {
            int duration;

            var subStart = (hasAccidental ? 3 : 1) + s.Count(c => c == '\'') + s.Count(c => c == ',');
            var subLen = s[s.Length - 1] == '.' ? s.Length - 1 - subStart : s.Length - subStart;

            if (int.TryParse(s.Substring(subStart, subLen), out duration))
                return duration;
            else
                throw new InvalidOperationException($"Cannot parse value \"{s}\" to valid note duration. Please verify your source file.");
        }

        private static readonly IDictionary<string, LilypondKeyword> LilypondKeywordDictionary = new Dictionary<string, LilypondKeyword>
        {
            {@"\relative",    LilypondKeyword.Relative},
            {@"\clef",        LilypondKeyword.Clef},
            {@"\tempo",       LilypondKeyword.Tempo},
            {@"\time",        LilypondKeyword.Time},
            {@"\repeat",      LilypondKeyword.Repeat},
            {@"\alternative", LilypondKeyword.Alternative},
            {"{",             LilypondKeyword.BracketOpen},
            {"}",             LilypondKeyword.BracketClose}
        };

        private static readonly IDictionary<char, Pitch> PitchDictionary = new Dictionary<char, Pitch>
        {
            {'a', Pitch.A},
            {'b', Pitch.B},
            {'c', Pitch.C},
            {'d', Pitch.D},
            {'e', Pitch.E},
            {'f', Pitch.F},
            {'g', Pitch.G},
            {'r', Pitch.Rest }
        };

        // TODO: Add default (don't know if lilypond supports it)
        private static readonly IDictionary<string, Accidental> AccidentalDictionary = new Dictionary<string, Accidental>
        {
            {"is", Accidental.Sharp},
            {"es", Accidental.Flat}
        };

        private static readonly IDictionary<string, Clef> ClefDictionary = new Dictionary<string, Clef>
        {
            {"treble", Clef.Treble},
            {"alt",    Clef.Alt},
            {"tenor",  Clef.Tenor},
            {"bass",   Clef.Bass}
        };
    }
}