using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            var totalSacks = rucksacks.Length;
            var sumOfPriorities = 0;

            for (var i = 0; i < totalSacks; i += 3)
            {
                var sack1 = rucksacks[i].Distinct().ToArray();
                var sack2 = rucksacks[i + 1].Distinct().ToArray();
                var sack3 = rucksacks[i + 2].Distinct().ToArray();
                var checkedItems = new Dictionary<char, int>();

                var minSack = Math.Min(sack1.Length, sack2.Length) == sack1.Length ?
                    (Math.Min(sack1.Length, sack3.Length) == sack1.Length ? new Tuple<int, char[]>(1, sack1) : new Tuple<int, char[]>(3, sack3)) :
                    (Math.Min(sack2.Length, sack3.Length) == sack2.Length ? new Tuple<int, char[]>(2, sack2) : new Tuple<int, char[]>(3, sack3));
                    
                var middleSack = minSack.Item1 == 1 ?
                        (Math.Min(sack2.Length, sack3.Length) == sack2.Length ? new Tuple<int, char[]>(2, sack2) : new Tuple<int, char[]>(3, sack3)) :
                        (minSack.Item1 == 2 ?
                            (Math.Min(sack1.Length, sack3.Length) == sack1.Length ? new Tuple<int, char[]>(1, sack1) : new Tuple<int, char[]>(3, sack3)) : 
                            (Math.Min(sack2.Length, sack1.Length) == sack2.Length ? new Tuple<int, char[]>(2, sack2) : new Tuple<int, char[]>(1, sack1)));
                            
                var largestSack = (minSack.Item1 == 1 && middleSack.Item1 == 2) || (minSack.Item1 == 2 && middleSack.Item1 == 1) ? sack3 :
                    ((minSack.Item1 == 1 && middleSack.Item1 == 3) || (minSack.Item1 == 3 && middleSack.Item1 == 1) ? sack2 : 
                    sack1);
                    
                var foundItem = false;
                for (int j=0; j < minSack.Item2.Length; j++)
                {
                    var minSackItem = minSack.Item2[j];
                    if (checkedItems.ContainsKey(minSackItem))
                    {
                        var numOfChecks = checkedItems[minSackItem];
                        if (numOfChecks == 2)
                        {
                            sumOfPriorities += GetPriority(minSackItem);
                            foundItem = true;
                            break;
                        }
                        checkedItems[minSackItem] = numOfChecks + 1;
                    }
                    else
                    {
                        checkedItems.Add(minSackItem, 1);
                    }

                    var middleSackItem = middleSack.Item2[j];
                    if (checkedItems.ContainsKey(middleSackItem))
                    {
                        var numOfChecks = checkedItems[middleSackItem];
                        if (numOfChecks == 2)
                        {
                            sumOfPriorities += GetPriority(middleSackItem);
                            foundItem = true;
                            break;
                        }
                        checkedItems[middleSackItem] = numOfChecks + 1;
                    }
                    else
                    {
                        checkedItems.Add(middleSackItem, 1);
                    }

                    var largestSackItem = largestSack[j];
                    if (checkedItems.ContainsKey(largestSackItem))
                    {
                        var numOfChecks = checkedItems[largestSackItem];
                        if (numOfChecks == 2)
                        {
                            sumOfPriorities += GetPriority(largestSackItem);
                            foundItem = true;
                            break;
                        }
                        checkedItems[largestSackItem] = numOfChecks + 1;
                    }
                    else
                    {
                        checkedItems.Add(largestSackItem, 1);
                    }
                }

                if (!foundItem)
                {
                    for (int j = minSack.Item2.Length; j < middleSack.Item2.Length; j++)
                    {
                        var middleSackItem = middleSack.Item2[j];
                        if (checkedItems.ContainsKey(middleSackItem))
                        {
                            var numOfChecks = checkedItems[middleSackItem];
                            if (numOfChecks == 2)
                            {
                                sumOfPriorities += GetPriority(middleSackItem);
                                foundItem = true;
                                break;
                            }
                            checkedItems[middleSackItem] = numOfChecks + 1;
                        }
                        else
                        {
                            checkedItems.Add(middleSackItem, 1);
                        }

                        var largestSackItem = largestSack[j];
                        if (checkedItems.ContainsKey(largestSackItem))
                        {
                            var numOfChecks = checkedItems[largestSackItem];
                            if (numOfChecks == 2)
                            {
                                sumOfPriorities += GetPriority(largestSackItem);
                                foundItem = true;
                                break;
                            }
                            checkedItems[largestSackItem] = numOfChecks + 1;
                        }
                        else
                        {
                            checkedItems.Add(largestSackItem, 1);
                        }
                    }
                }

                if (!foundItem)
                {
                    for (int j = middleSack.Item2.Length; j < largestSack.Length; j++)
                    {
                        var largestSackItem = largestSack[j];
                        if (checkedItems.ContainsKey(largestSackItem))
                        {
                            var numOfChecks = checkedItems[largestSackItem];
                            if (numOfChecks == 2)
                            {
                                sumOfPriorities += GetPriority(largestSackItem);
                                break;
                            }
                            checkedItems[largestSackItem] = numOfChecks + 1;
                        }
                        else
                        {
                            checkedItems.Add(largestSackItem, 1);
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
