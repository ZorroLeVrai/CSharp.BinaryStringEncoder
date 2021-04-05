using System;
using System.Text;

namespace BinaryStringEncoder
{
    /// <summary>
    /// Transform string message into binary ASCII code
    /// then for each sequence convert to the following format
    /// "[the bit encontered] [nb repetition of the bit encontered]
    /// "CC" => "1:1 0:4 1:3 0:4 1:2"
    /// </summary>
    class RepetitionEncoder : IEncoder
    {
        internal string Message { get; private set; }

        public RepetitionEncoder(string message)
        {
            Message = message;
        }

        public string Encode()
        {
            var binaryMessage = new StringBuilder();
            var outputMessage = new StringBuilder();

            var LastBit = default(char);

            foreach (char c in Message)
            {
                binaryMessage.Append(Convert.ToString(c, 2).PadLeft(7, '0'));
            }

            int nbRepetition = 0;
            foreach (char bit in binaryMessage.ToString())
            {
                if (bit != LastBit)
                {
                    if (nbRepetition > 0)
                        AppendRepetition(nbRepetition);
                    outputMessage.Append(bit);
                    nbRepetition = 1;
                }
                else
                {
                    ++nbRepetition;
                }

                LastBit = bit;
            }
            AppendRepetition(nbRepetition);

            return outputMessage.ToString().TrimEnd();

            StringBuilder AppendRepetition(int nbRepetition) => outputMessage.Append($":{nbRepetition} ");
        }
    }
}
