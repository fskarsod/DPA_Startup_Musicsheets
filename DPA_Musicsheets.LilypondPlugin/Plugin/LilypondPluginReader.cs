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

            var sheetString = $"\\relative c {{{Environment.NewLine}";

            sheetString += sheet.ToLilypond();

            return sheetString + "}" + Environment.NewLine;
        }
    }
}