using System;

namespace BinaryStringEncoder
{
    class Program
    {
        static void Main(string[] args)
        {
            var message = "CC";
            Console.WriteLine(new UnaryEncoder(message).Encode());
            Console.WriteLine(new RepetitionEncoder(message).Encode());
        }
    }
}
