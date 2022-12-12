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
            var logs = File.ReadAllLines(@"aoc.txt");
            var all = new Dictionary<Guid, Dir>();

            Dir current = null;

            foreach (var line in logs)
            {
                var command = line.Split(' ');
                if (command[0][0] == '$')
                {
                    var isLs = string.Equals(command[1], "ls");
                    if (!isLs)
                    {
                        if (current != null && !all.ContainsKey(current.Id))
                        {
                            all.Add(current.Id, current);
                        }

                        var name = command[2];
                        if (string.Equals(name, ".."))
                        {
                            current = current.Parent;
                        }
                        else if (current != null)
                        {
                            current = current.Dirs[name];
                        }
                        else
                        {
                            current = new Dir {
                                Id = Guid.NewGuid(),
                                Files = new List<Fil>(),
                                Dirs = new Dictionary<string, Dir>()
                            };
                        }
                    }
                }
                else
                {
                    var isDir = string.Equals(command[0], "dir");
                    var name = command[1];
                    if (isDir)
                    {
                        current.Dirs.Add(name, new Dir {
                            Files = new List<Fil>(),
                            Dirs = new Dictionary<string, Dir>(),
                            Parent = current,
                            Id = Guid.NewGuid()
                        });
                    }
                    else
                    {
                        current.Files.Add(new Fil {
                            Name = name,
                            Size = long.Parse(command[0])
                        });
                    }
                }
            }

            if (current != null && !all.ContainsKey(current.Id))
            {
                all.Add(current.Id, current);
            }

            long totalSize = 0;
            long space = 30000000;
            long currentMinSize = 70000000;
            long unusedSpace = currentMinSize;

            foreach (var x in all)
            {
                var sum = GetSubDirSize(x.Value, all, 0);  
                if (x.Value.Parent == null) {
                    totalSize = sum;
                    unusedSpace = currentMinSize - sum;
                }

                if (unusedSpace + sum >= space) {
                    currentMinSize = Math.Min(currentMinSize, sum);
                }
            }

            Console.WriteLine(currentMinSize);
        }

        private static long GetSubDirSize(Dir value, Dictionary<Guid, Dir> all, long sum)
        {
            sum += value.Files.Sum(f => f.Size);

            var children = value.Dirs;
            foreach (var y in children)
            {
                sum = GetSubDirSize(y.Value, all, sum);
            } 

            return sum;       
        }
    }

    class Dir
    {
        public Guid Id { get; set; }
        public Dictionary<string, Dir> Dirs { get; set; }
        public List<Fil> Files { get; set; }
        public Dir Parent { get; set; }
    }

    class Fil
    {
        public string Name { get; set; }
        public long Size { get; set;  }
    }
}
