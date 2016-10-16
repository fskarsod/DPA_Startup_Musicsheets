using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Model;
using System.Collections.Generic;
using DPA_Musicsheets.Core.Util;
using PSAMControlLibrary;


namespace DPA_Musicsheets.VisualNotes
{
    public class VisualNoteReaderPlugin : IPluginReader<IEnumerable<MusicalSymbol>>
    {
        private readonly IVisualNoteVisitor _visitor;

        public VisualNoteReaderPlugin(IVisualNoteVisitor visitor)
        {
            _visitor = visitor;
        }

        public IEnumerable<MusicalSymbol> ReadSheet(Sheet sheet)
        {
            // todo: fixme Tracks[1]
            foreach (var component in sheet.Tracks[1].GetMusicComponents())
            {
                component.Accept(_visitor);
                yield return _visitor.Result;
            }
        }
    }
}
