using System;
using DPA_Musicsheets.Core.Builder;
using DPA_Musicsheets.Core.Model.Enum;
using Xunit;

namespace DPA_Musicsheets.Core.Tests.Builder
{
    [Trait("Core", "Builder")]
    public class NoteBuilderTests
    {
        [Fact]
        public void NoteBuilder_CompleteNote()
        {
            var sut = new NoteBuilder();
            sut.SetAccidental(Accidental.Flat)
                .SetOctave(1)
                .SetPitch(Pitch.D)
                .SetDuration(4)
                .HasDot();
            var actual = sut.Build();
            Assert.Equal(Accidental.Flat, actual.Accidental);
            Assert.Equal(Pitch.D, actual.Pitch);
            Assert.Equal(1, actual.Octave);
            Assert.Equal(4, actual.Duration);
            Assert.Equal(true, actual.HasDot);
        }

        [Fact]
        public void NoteBuilder_NoAccidental()
        {
            var sut = new NoteBuilder();
            sut.SetOctave(1)
                .SetPitch(Pitch.D)
                .SetDuration(4)
                .HasDot();
            var actual = sut.Build();
            Assert.Equal(Accidental.Default, actual.Accidental);
            Assert.Equal(Pitch.D, actual.Pitch);
            Assert.Equal(1, actual.Octave);
            Assert.Equal(4, actual.Duration);
            Assert.Equal(true, actual.HasDot);
        }

        [Fact]
        public void NoteBuilder_NoOctave()
        {
            var sut = new NoteBuilder();
            sut.SetPitch(Pitch.A)
                .SetAccidental(Accidental.Flat)
                .SetDuration(4)
                .HasDot();
            Assert.Throws<InvalidOperationException>(() => sut.Build());
        }

        [Fact]
        public void NoteBuilder_NoDuration()
        {
            var sut = new NoteBuilder();
            sut.SetOctave(1)
                .SetAccidental(Accidental.Flat)
                .SetPitch(Pitch.A)
                .HasDot();
            Assert.Throws<InvalidOperationException>(() => sut.Build());
        }
    }
}
