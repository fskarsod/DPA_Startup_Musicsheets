using System;
using System.Linq;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Model;

namespace DPA_Musicsheets.LilypondPlugin.Plugin
{
    public class LilypondPluginReader : IPluginReader<string>
    {
        public string ReadSheet(Sheet sheet)
        {
            // I don't know how to write multitrack so I'll just ignore anything other than track 1 -- we don't read more than 1 anyway afaik
            // TODO: Incomplete. Doesn't do repetition, alternatives or keywords... yet.

            var sheetString = "";

            foreach (var componentProvider in sheet.Tracks[0].MusicComponentProviders)
            {
                sheetString = componentProvider.GetMusicComponents()
                    .Aggregate(sheetString, (current, component) => current + component.AsString() + " ");

                sheetString += Environment.NewLine;
            }

            return "";
        }
    }
}