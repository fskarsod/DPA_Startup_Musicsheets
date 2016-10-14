using System;
using System.Linq;
using DPA_Musicsheets.Core.Builder;
using DPA_Musicsheets.Core.Builder.Interface;
using DPA_Musicsheets.Core.Model;
using Xunit;

namespace DPA_Musicsheets.Core.Tests.Builder
{
    [Trait("Core", "Builder")]
    public class BarBuilderTests
    {
        private BaseNote BarBuilderHelper_DefaultNote()
        {
            return new Rest { Duration = 4, HasDot = false };
        }

        [Fact]
        public void BarBuilder_CompleteBar()
        {
            var param = BarBuilderHelper_DefaultNote();
            var sut = new BarBuilder();
            sut.SetTimeSignature(new TimeSignature(4, 4))
                .AddComponent(param)
                .AddComponent(param)
                .AddComponent(param)
                .AddComponent(param);
            var actual = sut.Build();
            Assert.Equal(5, actual.MusicComponents.Count);
            Assert.Equal(4, actual.MusicComponents.OfType<BaseNote>().Count());
        }

        [Fact]
        public void BarBuilder_NoTimeSignature()
        {
            var param = BarBuilderHelper_DefaultNote();
            var sut = new BarBuilder();
            sut.AddComponent(param);
            Assert.Throws<InvalidOperationException>(() => sut.Build());
        }

        [Fact]
        public void BarBuilder_NotEnoughNotes()
        {
            var param = BarBuilderHelper_DefaultNote();
            var sut = new BarBuilder();
            sut.SetTimeSignature(new TimeSignature(4, 4))
                .AddComponent(param)
                .AddComponent(param);
            Assert.Throws<InvalidOperationException>(() => sut.Build());
        }

        [Fact]
        public void BarBuilder_TooManyNotes()
        {
            var param = BarBuilderHelper_DefaultNote();
            var sut = new BarBuilder();
            sut.SetTimeSignature(new TimeSignature(4, 4))
                .AddComponent(param)
                .AddComponent(param)
                .AddComponent(param)
                .AddComponent(param)
                .AddComponent(param);
            Assert.Throws<InvalidOperationException>(() => sut.Build());
        }

        [Fact]
        public void BarBuilder_ChangeInTimeSignature()
        {
            var param = BarBuilderHelper_DefaultNote();
            var sut = new BarBuilder();
            sut.SetTimeSignature(new TimeSignature(4, 4))
                .AddComponent(param)
                .AddComponent(param);
            Assert.Throws<InvalidOperationException>(() => sut.SetTimeSignature(new TimeSignature(4, 4)));
        }
    }
}
