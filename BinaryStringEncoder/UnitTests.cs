using Xunit;

namespace BinaryStringEncoder
{
    public class UnitTests
    {
        [Fact]
        public void RepetitionEncoderTest()
        {
            Assert.Equal("1:1 0:4 1:3 0:4 1:2", new RepetitionEncoder("CC").Encode());
        }

        [Fact]
        public void FastRepetitionEncoderTest()
        {
            Assert.Equal("1:1 0:4 1:3 0:4 1:2", new FastRepetitionEncoder("CC").Encode());
        }

        [Fact]
        public void UnaryEncoderTest()
        {
            Assert.Equal("0 0 00 0000 0 000 00 0000 0 00", new UnaryEncoder("CC").Encode());
        }
    }
}
