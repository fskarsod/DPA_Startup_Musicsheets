using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Model;
using System.Collections.Generic;
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
            // todo: fixme
            foreach (var provider in sheet.Tracks[1].MusicComponentProviders)
            {
                foreach (var component in provider.GetMusicComponents())
                {
                    component.Accept(_visitor);
                    yield return _visitor.Result;
                }
            }
        }
    }
}
