using System;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Model;
using Sanford.Multimedia.Midi;
using SanfordTrack = Sanford.Multimedia.Midi.Track;

namespace DPA_Musicsheets.MidiPlugin.Util
{
    public class MidiPluginVisitor : IMusicComponentVisitor // For MidiPluginWriter
    {
        private readonly SanfordTrack _sandfordTrack;

        public MidiPluginVisitor()
        {
            _sandfordTrack = new SanfordTrack();
        }

        public void Visit(IMusicComponent musicComponent)
        {
            throw new NotImplementedException();
        }

        public void Visit(BarBoundary barBoundary)
        {
            throw new NotImplementedException();
        }

        public void Visit(BaseNote baseNote)
        {
            throw new NotImplementedException();
        }

        public void Visit(Note note)
        {
            // todo: NoteToMidiStrategy?

            var builder = new ChannelMessageBuilder();
            builder.Command = ChannelCommand.NoteOn;
            builder.Data1 = 0; // todo: get keycode (?)
            builder.Data2 = 100; // Intensity i believe(?)
            builder.Build();
            _sandfordTrack.Insert(0, builder.Result);
            builder.Command = ChannelCommand.NoteOn; // or noteOff if you want
            builder.Data1 = 0; // todo: get keycode (?)
            builder.Data2 = 0; // Intensity i believe(?) so 0 if NoteOn
            _sandfordTrack.Insert(0, builder.Result);
        }
    }
}
