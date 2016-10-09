using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DPA_Musicsheets.Core.Builder;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Model;
using DPA_Musicsheets.LilypondPlugin.Enum;

namespace DPA_Musicsheets.LilypondPlugin.Plugin
{
    public class LilypondPluginWriter : IPluginWriter<string>
    {
        public Sheet WriteSheet(string source)
        {
            var elements = SplitSource(source);
            var track = new TrackBuilder();

            for (var i = 0; i < elements.Count(); i++)
            {
                switch (ToKeyword(elements[i]))
                {
                    case LilypondKeyword.None:
                        break;
                    case LilypondKeyword.Relative:
                        break;
                    case LilypondKeyword.Clef:
                        break;
                    case LilypondKeyword.Tempo:
                        break;
                    case LilypondKeyword.Time:
                        break;
                    case LilypondKeyword.Repeat:
                        break;
                    case LilypondKeyword.Alternative:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return new Sheet();
        }

        private void SetTimeSignature(TimeSignature timeSignature)
        {
            
        }

        private static string[] SplitSource(string source)
        {
            // some empty lines remain, but we'll just ignore them when parsing for now.
            return source.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static LilypondKeyword ToKeyword(string s)
        {
            switch (s)
            {
                case @"\relative":
                    return LilypondKeyword.Relative;
                case @"\clef":
                    return LilypondKeyword.Clef;
                case @"\tempo":
                    return LilypondKeyword.Tempo;
                case @"\time":
                    return LilypondKeyword.Time;
                case @"\repeat":
                    return LilypondKeyword.Repeat;
                case @"\alternative":
                    return LilypondKeyword.Alternative;
                default:
                    // TODO: Possibly handle this better.
                    return LilypondKeyword.None;
            }
        }
    }
}