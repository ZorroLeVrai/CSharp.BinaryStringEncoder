using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace BinaryStringEncoder
{
    /// <summary>
    /// Transform string message into binary ASCII code
    /// then for each sequence convert to the following format
    /// "[string coding the bit] [0 repeated to signal how many bit repetitions]
    /// "CC" => "0 0 00 0000 0 000 00 0000 0 00"
    /// </summary>
    class UnaryEncoder : IEncoder
    {
        internal string Message { get; private set; }

        public UnaryEncoder(string message)
        {
            Message = message;
        }

        public string Encode()
        {
            //Get binary code
            string encode = Message.Select(x => (int)x)
                .Select(x => Convert.ToString(x, 2).PadLeft(7, '0'))
                .Aggregate((x, y) => x + y);

            //regex replacements
            //$1 captures the first group
            encode = Regex.Replace(encode, @"(0+)", "00 $1 ");
            encode = Regex.Replace(encode, @"(1+)", "0 $1 ")
                .Replace('1', '0');

            return encode.TrimEnd();
        }
    }
}
