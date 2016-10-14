using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using DPA_Musicsheets.Core.Builder;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Model;
using DPA_Musicsheets.Core.Model.Enum;
using DPA_Musicsheets.LilypondPlugin.Enum;

namespace DPA_Musicsheets.LilypondPlugin.Plugin
{
    public class LilypondPluginWriter : IPluginWriter<string>
    {
        public Sheet WriteSheet(string source)
        {
            var elements = SplitSource(source);
            var track = new TrackBuilder();

            foreach (var e in elements)
            {
                switch (ToKeyword(e))
                {
                    case LilypondKeyword.None:
                        AddNoteToTrack(e);
                        break;
                    case LilypondKeyword.Time:
                        SetTimeSignature(e);
                        break;
                    case LilypondKeyword.Relative:
                    case LilypondKeyword.Clef:
                    case LilypondKeyword.Tempo:
                    case LilypondKeyword.Repeat:
                    case LilypondKeyword.Alternative:
                        throw new NotImplementedException();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return new Sheet();
        }

        private void AddNoteToTrack(string noteString)
        {
            var pitch = ToPitch(noteString.ToLower()[0]);
            var accidental = ToAccidental(noteString.Substring(1, 2));
            var octaveOffset = GetOctaveOffset(noteString);
            var duration = GetDuration(noteString, (accidental != Accidental.Natural));
            var hasDot = noteString.Contains('.');
        }

        private void SetTimeSignature(string timeSignatureString)
        {
            
        }

        private static string[] SplitSource(string source)
        {
            // some empty lines remain, but we'll just ignore them when parsing for now.
            return source.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static LilypondKeyword ToKeyword(string s)
        {
            return LilypondKeywordDictionary.ContainsKey(s.ToLower()) ? LilypondKeywordDictionary[s] : LilypondKeyword.None;
        }

        private static Pitch? ToPitch(char c)
        {
            if (PitchDictionary.ContainsKey(c))
                return PitchDictionary[c];

            return null;
        }

        private static Accidental ToAccidental(string s)
        {
            return AccidentalDictionary.ContainsKey(s.ToLower()) ? AccidentalDictionary[s] : Accidental.Natural;
        }

        private static int GetDuration(string s, bool hasAccidental)
        {
            var hasDot = s.Contains('.');
            string durationString;
            int duration;

            // somewhat awkward to retrieve, but whatever.
            if (hasDot && hasAccidental)
                durationString = s.Substring(3, s.Length - 1);
            else if (hasDot)
                durationString = s.Substring(1, s.Length - 1);
            else if (hasAccidental)
                durationString = s.Substring(3, s.Length);
            else
                durationString = s.Substring(1, s.Length);

            if (int.TryParse(durationString, out duration))
                return duration;
            else
                throw new ArgumentException();
        }

        private static int GetOctaveOffset(string s)
        {
            if (s.Contains(','))
                return s.Count(c => c == ',') * -1;
            else if (s.Contains('\''))
                return s.Count(c => c == '\'');
            else
                return 0;
        }

        private static readonly IDictionary<string, LilypondKeyword> LilypondKeywordDictionary = new Dictionary<string, LilypondKeyword>
        {
            { @"\relative",    LilypondKeyword.Relative },
            { @"\clef",        LilypondKeyword.Clef },
            { @"\tempo",       LilypondKeyword.Tempo },
            { @"\time",        LilypondKeyword.Time },
            { @"\repeat",      LilypondKeyword.Repeat },
            { @"\alternative", LilypondKeyword.Alternative }
        };

        private static readonly IDictionary<char, Pitch> PitchDictionary = new Dictionary<char, Pitch>
        {
            { 'a', Pitch.A },
            { 'b', Pitch.B },
            { 'c', Pitch.C },
            { 'd', Pitch.D },
            { 'e', Pitch.E },
            { 'f', Pitch.F },
            { 'g', Pitch.G }
        };

        // TODO: Add default (don't know if lilypond supports it)
        private static readonly IDictionary<string, Accidental> AccidentalDictionary = new Dictionary<string, Accidental>
        {
            { "is", Accidental.Sharp},
            { "es", Accidental.Flat }
        };
    }
}