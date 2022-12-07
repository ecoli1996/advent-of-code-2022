using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
	class Program
	{
        static void Main(string[] args)
        {
            var rows = File.ReadAllLines(@"aoc.txt");
            // column #, column
            var crates = new Dictionary<int, Stack<char>>();
            var directions = new Stack<string>();
            var totalRows = rows.Count();
            var totalColumns = 0;

            for (int i=totalRows-1; i >= 0; i--)
            {
                var row = rows[i];

                if (!string.IsNullOrWhiteSpace(row))
                {
                    if (row[0] == 'm') directions.Push(row);
                    else if (totalColumns == 0) {
                        var rowTrim = row.TrimEnd();
                        totalColumns = int.Parse(rowTrim.Last().ToString());
                    }
                    else if(!string.IsNullOrWhiteSpace(row))
                    {
                        var cratesInRow = row.Split(' ');
                        var index = 0;
                        for (int j = 1; j <= totalColumns; j++)
                        {
                            if (!crates.ContainsKey(j)) crates.Add(j, new Stack<char>());

                            var crate = cratesInRow[index];
                            if (crate == string.Empty) index += 4;
                            else
                            {
                                var crateLetter = crate[1];
                                crates[j].Push(crateLetter);
                                index++;
                            }
                        }
                    }
                }
            }


            foreach (var direction in directions)
            {
                var directionSplit = direction.Split(' ');
                var move = int.Parse(directionSplit[1]);
                var from = int.Parse(directionSplit[3]);
                var to = int.Parse(directionSplit[5]);

                for (int j = 0; j < move; j++)
                {
                    var crateToMove = crates[from].Pop();
                    crates[to].Push(crateToMove);
                }
            }

            for (int j = 1; j <= totalColumns; j++)
            {
                Console.Write(crates[j].Pop());
            }
        }
    }
}
