using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Builder;
using DPA_Musicsheets.Core.Model;
using Xunit;

namespace DPA_Musicsheets.Core.Tests.Builder
{
    [Trait("Core","Builder")]
    public class TrackBuilderTests
    {
        private BaseNote TrackBuilderHelper_DefaultNote()
        {
            return new Rest { Duration = 4, HasDot = false };
        }

        [Fact]
        public void TrackBuilder_AggregateNoteValid()
        {
            var timeSig = new TimeSignature(4, 4);
            var param = TrackBuilderHelper_DefaultNote();
            var sut = new TrackBuilder();
            sut.SetName("Test_me_not")
                .Add(timeSig, param)
                .Add(timeSig, param)
                .Add(timeSig, param)
                .Add(timeSig, param);
            var actual = sut.Build();
            Assert.Equal(1, actual.MusicComponentProviders.Count);
            Assert.Equal(5, actual.MusicComponentProviders.First().GetMusicComponents().Count());
        }

        [Fact]
        public void TrackBuilder_AggregateNoteInvalid()
        {
            var timeSig = new TimeSignature(4, 4);
            var param = TrackBuilderHelper_DefaultNote();
            var sut = new TrackBuilder();
            sut.SetName("Test_me_not")
                .Add(timeSig, param)
                .Add(timeSig, param)
                .Add(timeSig, param)
                .Add(timeSig, param)
                .Add(timeSig, param);
            Assert.Throws<InvalidOperationException>(() => sut.Build());
        }
    }
}
