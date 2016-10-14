using System;
using DPA_Musicsheets.Core.Util;
using Xunit;

namespace DPA_Musicsheets.Core.Tests
{
    [Trait("Core", "Util")]
    public class UtilTests
    {
        public static TheoryData<bool, long> IsPowerOfTwoTestData => new TheoryData<bool, long>
        {
            { true, 1 },
            { true, 2 },
            { false, 3 },
            { true, 4 },
            { false, 5 },
            { false, 6 },
            { false, 7 },
            { true, 8 },
        };

        [Theory, MemberData(nameof(IsPowerOfTwoTestData))]
        public void IsPowerOfTwoTests(bool expected, long actual)
        {
            Assert.Equal(expected, MathClass.IsPowerOfTwo(actual));
        }
    }
}
