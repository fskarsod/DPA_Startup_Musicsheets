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
            var pitch = ToPitch("" + noteString[0]);
            var accidental = ToAccidental(noteString.Substring(1, 2));
            if (noteString[1] == ',' || noteString[1] == '\'')
            {
                // up the octave // or count , or '  in string
            }
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

        private static Pitch? ToPitch(string s)
        {
            if (PitchDictionary.ContainsKey(s.ToLower()))
                return PitchDictionary[s];

            return null;
        }

        private static Accidental ToAccidental(string s)
        {
            return AccidentalDictionary.ContainsKey(s.ToLower()) ? AccidentalDictionary[s] : Accidental.Natural;
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

        private static readonly IDictionary<string, Pitch> PitchDictionary = new Dictionary<string, Pitch>
        {
            { "a", Pitch.A },
            { "b", Pitch.B },
            { "c", Pitch.C },
            { "d", Pitch.D },
            { "e", Pitch.E },
            { "f", Pitch.F },
            { "g", Pitch.G }
        };

        // TODO: Add default (don't know if lilypond supports it)
        private static readonly IDictionary<string, Accidental> AccidentalDictionary = new Dictionary<string, Accidental>
        {
            { "is", Accidental.Sharp},
            { "es", Accidental.Flat }
        };
    }
}