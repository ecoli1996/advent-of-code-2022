using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
	class Program
	{
		static void Main(string[] args)
		{
            var lines = File.ReadAllLines(@"aoc.txt");
            var maxCals = 0;
            var secondMostCals = 0;
            var thirdMostCals = 0;

            var currentCals = 0;
            var lineNumber = 1;
            var totalLines = lines.Count();

            foreach (var line in lines)
            {
                if (currentCals == 0 && lineNumber == totalLines)
                {
                    currentCals += int.Parse(line);
                }
                if (string.IsNullOrWhiteSpace(line) || lineNumber == totalLines)
                {
                    if (currentCals > maxCals)
                    {
                        var tempSecondMostCals = secondMostCals;
                        var tempMaxCals = maxCals;

                        maxCals = currentCals;
                        secondMostCals = tempMaxCals;
                        thirdMostCals = tempSecondMostCals;
                    }
                    else if (currentCals > secondMostCals)
                    {
                        var tempSecondMostCals = secondMostCals;

                        secondMostCals = currentCals;
                        thirdMostCals = tempSecondMostCals;

                    }
                    else if (currentCals > thirdMostCals)
                    {
                        thirdMostCals = currentCals;
                    }

                    currentCals = 0;
                }
                else
                {
                    currentCals += int.Parse(line);
                }

                lineNumber++;
            }

            Console.WriteLine($"1st: {maxCals}");
            Console.WriteLine($"2nd: {secondMostCals}");
            Console.WriteLine($"3rd: {thirdMostCals}");
            Console.WriteLine($"Sum: {maxCals + secondMostCals + thirdMostCals}");
        }
    }
}
