using System;
using System.IO;

namespace AdventOfCode
{
	class Program
	{
        static void Main(string[] args)
		{
            var sectionAssignments = File.ReadAllLines(@"aoc.txt");
            var numOfSubsets = 0;

            foreach (var assignment in sectionAssignments)
            {
                var pairs = assignment.Split(',');

                var elfRange1 = pairs[0].Split('-');
                var elfRange1_X = int.Parse(elfRange1[0]);
                var elfRange1_Y = int.Parse(elfRange1[1]);

                var elfRange2 = pairs[1].Split('-');
                var elfRange2_X = int.Parse(elfRange2[0]);
                var elfRange2_Y = int.Parse(elfRange2[1]);

                if (
                    elfRange1_X == elfRange2_X || // x1 == x2
                    elfRange1_Y == elfRange2_Y || // y1 == y2
                    elfRange1_X == elfRange2_Y || // x1 == y2
                    elfRange1_Y == elfRange2_X || // y1 == x2
                    (elfRange1_X >= elfRange2_X && elfRange1_Y <= elfRange2_Y) || // (x2, x1, y1, y2)
                    (elfRange2_X >= elfRange1_X && elfRange2_Y <= elfRange1_Y) || // (x1, x2, y2, y1)
                    (elfRange1_X <= elfRange2_X && elfRange1_Y >= elfRange2_X && elfRange2_Y >= elfRange1_Y) || // x1..(x2, y1, y2)..
                    (elfRange2_X <= elfRange1_X && elfRange2_Y >= elfRange1_X && elfRange1_Y >= elfRange2_Y) // x2.. (x1, y2, y1)..
                )
                {
                    numOfSubsets++;
                }
            }

            Console.WriteLine(numOfSubsets);
        }
    }
}
