using System.Collections;
using System.Text;

namespace BinaryStringEncoder
{
    /// <summary>
    /// Fast but more complex unary encoder
    /// Uses bitarrays - Doesn't rely heavily on strings
    /// </summary>
    class FastRepetitionEncoder : IEncoder
    {
        internal string Message { get; private set; }

        public FastRepetitionEncoder(string message)
        {
            Message = message;
        }

        public string Encode()
        {
            return new UnaryEncoder(new BitEncoder(Message).ToBinary()).ToUnary();
        }


        private class UnaryEncoder
        {
            internal BitArray Bits { get; private set; }

            internal UnaryEncoder(BitArray bitArray)
            {
                Bits = bitArray;
            }

            public string ToUnary()
            {
                var unaryCode = new StringBuilder();
                var nbBits = Bits.Length;
                var currentIndex = 0;

                while (currentIndex < nbBits)
                {
                    if (currentIndex > 0)
                        unaryCode.Append(' ');

                    //append current value
                    var currentBit = Bits[currentIndex++];
                    unaryCode.Append(currentBit ? '1' : '0');
                    var repetitionCount = 1;

                    //increment the repetition counter as long as we have the same bit
                    while (currentIndex < nbBits && currentBit == Bits[currentIndex])
                    {
                        ++repetitionCount;
                        ++currentIndex;
                    }

                    unaryCode.Append($":{repetitionCount}");
                }

                return unaryCode.ToString();
            }
        }

        private class BitEncoder
        {
            private const int ASCII_CODE_BITS = 7;

            internal byte[] ByteMessage { get; set; }
            private BitArray _bitArray;
            private bool _encodingDone;

            public BitEncoder(string message)
            {
                //encode all characters into ASCII codes
                ByteMessage = System.Text.Encoding.ASCII.GetBytes(message);

                //every ASCII code is encoded in 7 bits
                //false is the default value for every bit
                _bitArray = new BitArray(ASCII_CODE_BITS * ByteMessage.Length, false);

                _encodingDone = false;
            }

            public BitArray ToBinary()
            {
                EncodeInBinary();
                return _bitArray;
            }

            public string GetStringRepresentation()
            {
                var binaryText = new StringBuilder();

                EncodeInBinary();
                for (int i = 0; i < _bitArray.Length; ++i)
                {
                    binaryText.Append(_bitArray.Get(i) ? '1' : '0');
                }

                return binaryText.ToString();
            }

            private void EncodeInBinary()
            {
                if (_encodingDone)
                    return;

                //encode every ASCII into bits
                for (int i = 0; i < ByteMessage.Length; ++i)
                {
                    EncodeInBitArray(ByteMessage[i], ASCII_CODE_BITS * i);
                }

                _encodingDone = true;

                void EncodeInBitArray(byte currentAscii, int bitArrayPosition)
                {
                    var currentIndex = 6;

                    while (currentIndex >= 0 && currentAscii > 0)
                    {
                        if (currentAscii % 2 == 1)
                            _bitArray.Set(bitArrayPosition + currentIndex, true);
                        currentAscii /= 2;
                        --currentIndex;
                    }
                }
            }
        }
    }
}
