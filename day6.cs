using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
	class Program
	{
        static void Main(string[] args)
        {
            var stream = File.ReadAllText(@"aoc.txt");

            // letter, index
            var packetIndexes = new Dictionary<char, int>();
            var packetChecker = new HashSet<char>();
            var i = 0;
            var foundPack = false;
            var countOfLetters = 0;

            while (!foundPack)
            {
                var letter = stream[i];
                var unique = packetChecker.Add(letter);
                if (unique)
                {
                    packetIndexes.Add(letter, i);
                    countOfLetters++;
                    i++;
                }
                else
                {
                    var dupeI = packetIndexes[letter];
                    packetChecker = new HashSet<char>();
                    packetIndexes = new Dictionary<char, int>();
                    i = dupeI + 1;
                    countOfLetters = 0;
                }

                // if (countOfLetters == 4) in pt. 1
                if (countOfLetters == 14)
                {
                    foundPack = true;
                }
            }

            Console.WriteLine(i);
        }
    }
}
