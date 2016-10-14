using System;
using DPA_Musicsheets.Core.Model;
using Xunit;

namespace DPA_Musicsheets.Core.Tests.Model
{
    [Trait("Core", "Model")]
    public class LengthValueTests
    {
        public static TheoryData<double, int, int> TotalLengthValueTestData => new TheoryData<double, int, int>
        {
            { 1D/1D, 1, 1 },
            { 4D/4D, 4, 4 },
            { 6D/8D, 6, 8 }
        };

        [Theory, MemberData(nameof(TotalLengthValueTestData))]
        public void TimeSignature_TotalLengthValue(double expected, int numerator, int denominator)
        {
            var sut = new TimeSignature(numerator, denominator);
            Assert.Equal(expected, sut.TotalLengthValue);
        }

        public static TheoryData<double, int, bool> LengthValueTestData => new TheoryData<double, int, bool>
        {
            { 1D, 1, false },
            { 1.5D, 1, true },
            { 0.5D, 2, false},
            { 0.75D, 2, true },
            { 0.25D, 4, false},
            { 0.385D, 4, true },
            { 0.125D, 8, false },
            { 0.1875D, 8, true }
        };

        [Theory, MemberData(nameof(LengthValueTestData))]
        public void Rest_LengthValue(double expected, int duration, bool hasDot)
        {
            var sut = new Rest
            {
                Duration = duration,
                HasDot = hasDot
            };
            Assert.Equal(expected, sut.LengthValue, 0);
        }

        [Theory, MemberData(nameof(LengthValueTestData))]
        public void Note_LengthValue(double expected, int duration, bool hasDot)
        {
            var sut = new Note
            {
                Duration = duration,
                HasDot = hasDot
            };
            Assert.Equal(expected, sut.LengthValue, 0);
        }
    }
}
