using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
	class Program
	{
        // letters -> values -> ascii -> diff
        // a-z -> 1-26 -> 97-122 -> 96
        // A-Z -> 27-52 -> 65-90 -> 38

        static void Main(string[] args)
		{
            var rucksacks = File.ReadAllLines(@"aoc.txt");
            var sumOfPriorities = 0;

            foreach (var sack in rucksacks)
            {
                var itemCount = sack.Length;
                var compartment1 = sack.Substring(0, itemCount / 2);
                var compartment2 = sack.Substring(itemCount / 2);

                var checkedPrioritiesC1 = new HashSet<char>();
                var checkedPrioritiesC2 = new HashSet<char>();
                var checkedPrioritiesAll = new HashSet<char>();

                for (var i=0; i < compartment1.Length; i++)
                {
                    var c1Letter = compartment1[i];
                    var c1NotDuplicate = checkedPrioritiesC1.Add(c1Letter);
                    if (c1NotDuplicate)
                    {
                        var c1Added = checkedPrioritiesAll.Add(c1Letter);

                        if (!c1Added)
                        {
                            // Console.WriteLine($"Checking p val for {c1Letter}.");
                            sumOfPriorities += GetPriority(c1Letter);
                            break;
                        }
                    }

                    var c2Letter = compartment2[i];
                    var c2NotDuplicate = checkedPrioritiesC2.Add(c2Letter);
                    if (c2NotDuplicate)
                    {
                        var c2Added = checkedPrioritiesAll.Add(c2Letter);

                        if (!c2Added)
                        {
                            // Console.WriteLine($"Checking p val for {c2Letter}.");
                            sumOfPriorities += GetPriority(c2Letter);
                            break;
                        }
                    }
                }
            }

            Console.WriteLine(sumOfPriorities);
        }

        private static int GetPriority(char letter)
        {
            var asciiVal = (int)letter;
            if (asciiVal >= 97)
            {
                return asciiVal - 96;
            }

            return asciiVal - 38;
        }
    }
}
