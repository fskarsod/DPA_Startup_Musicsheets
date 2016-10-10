using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Model;
using Sequence = Sanford.Multimedia.Midi.Sequence;

namespace DPA_Musicsheets.MidiPlugin.Plugin
{
    public class MidiPluginReader : IPluginReader<Sequence>
    {
        public Sequence ReadSheet(Sheet sheet)
        {
            throw new NotImplementedException();
        }
    }
}
