using DPA_Musicsheets.Core.Model;
using DPA_Musicsheets.Core.Model.Enum;

namespace DPA_Musicsheets.Core.Builder.Sample
{
    public static class BuilderSample
    {
        public static void Main()
        {
            Track track = new TrackBuilder()
                .AddBar(bar => bar
                    .SetTimeSignature(new TimeSignature(4, 4))
                    .AddNote(note => note
                        .SetDuration(2)
                        .HasDot()
                        .SetAccidental(Accidental.Default)
                        .SetPitch(Pitch.B))
                //.AddNote(note => note
                //    .SetDuration(2)
                //    .SetPitch(Pitch.A))
                //.AddNote(note => note // todo: should throw error at run-time because a bar with time signature 4/4 cannot have a third note after two half notes.
                //    .SetDuration(2)
                //    .SetPitch(Pitch.A))
                        )
                .AddRepetition(repetition => repetition
                    .AddBar(bar => bar
                        .SetTimeSignature(new TimeSignature(4, 4))
                        .AddRest(rest => rest
                            .SetDuration(2))
                        .AddNote(note => note
                            .SetDuration(2)
                            .SetPitch(Pitch.B)))
                    .AddEnding(ending => ending
                        .SetRepeats(2)
                        .AddBar(bar => bar
                            .SetTimeSignature(new TimeSignature(4, 4))
                            .AddNote(note => note
                                .SetDuration(2)
                                .HasDot()
                                .SetAccidental(Accidental.Default)
                                .SetPitch(Pitch.B))
                            .AddNote(note => note
                                .SetDuration(2)
                                .HasDot()
                                .SetAccidental(Accidental.Default)
                                .SetPitch(Pitch.B)))))
                .Build();
        }
    }
}