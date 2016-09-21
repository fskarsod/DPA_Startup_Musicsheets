namespace DPA_Musicsheets.Core.Builder.Sample
{
    static class BuilderSample
    {
        public class Track { }

        public class Bar { }

        public class Repetition { }

        public class Note { }

        public class Rest { }

        static void Main()
        {
            Track track = new TrackBuilder<Track>()
                .AddBar<Bar>(bar => bar
                    .AddBarBoundary()
                    .AddNote<Note>(note => note
                        .SetPitch(1)
                        .SetDuration(2)
                        .SetAccidental(3))
                    .AddRest<Rest>(rest => rest
                        .HasDot()
                        .SetDuration(2)))
                .AddRepetition<Repetition>(repetition => repetition
                    .AddBar<Bar>(bar => bar
                        .AddBarBoundary()
                        .AddNote<Note>(note => note
                            .SetDuration(2)
                            .SetPitch(2)))
                    .AddBar<Bar>(bar => bar
                        .AddBarBoundary()
                        .AddNote<Note>(note => note
                            .SetDuration(2)
                            .SetPitch(2))))
                .Build();
        }
    }
}
