using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using DPA_Musicsheets.Core.Builder;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Model;
using DPA_Musicsheets.Core.Model.Enum;
using DPA_Musicsheets.LilypondPlugin.Enum;
using DPA_Musicsheets.LilypondPlugin.Parser;

namespace DPA_Musicsheets.LilypondPlugin.Plugin
{
    public class LilypondPluginWriter : IPluginWriter<string>
    {
        public Sheet WriteSheet(string source)
        {
            var tokens = Tokenize(source);
            var track = new TrackBuilder();
            TimeSignature timeSig = null;
            Tempo tempo = null;
            var relativeOctave = 3;
            var relativeNote = '\0';
            Clef? clef;
            var bracketState = 0; // +1 for each opening, -1 for each closing... sorry for ghetto code

            // TODO: Clean this up
            for (var i = 0; i < tokens.Length; i++)
            {
                switch (LilypondParser.GetKeyword(tokens[i]))
                {
                    case null: // not a keyword
                        if (LilypondParser.IsNote(tokens[i]))
                            AddNoteToBar(LilypondParser.CreateNoteFromString(tokens[i], ref relativeOctave, ref relativeNote), track, timeSig, ref tempo);
                        break;
                    case LilypondKeyword.Time:
                        timeSig = LilypondParser.GetTimeSignature(tokens[++i]);
                        break;
                    case LilypondKeyword.Relative:
                        relativeNote = tokens[++i][0];
                        relativeOctave += LilypondParser.GetOctaveOffset(tokens[i], relativeNote);
                        break;
                    case LilypondKeyword.Clef:
                        clef = LilypondParser.GetClef(tokens[++i]);
                        // todo: set clef on the track
                        break;
                    case LilypondKeyword.BracketOpen:
                        bracketState++;
                        break;
                    case LilypondKeyword.BracketClose:
                        bracketState--;
                        break;
                    case LilypondKeyword.Tempo:
                        tempo = LilypondParser.GetTempo(tokens[++i]);
                        break;
                    case LilypondKeyword.Repeat:
                    case LilypondKeyword.Alternative:
                        // todo : implement these...
                        break;
                }
            }

            var t = track.Build();
            var s = new Sheet
            {
                Tracks = new[] {t}
            };


            return s;
        }

        private static void AddNoteToBar(BaseNote note, TrackBuilder track, TimeSignature timeSignature, ref Tempo tempo)
        {
            track.Add(timeSignature, note, tempo);
            tempo = null; // one time use, I apologize for my plebness.
        }

        private static string[] Tokenize(string source)
        {
            // some empty lines remain, but we'll just ignore them when parsing for now.
            return source.Replace("\r\n", " ").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}