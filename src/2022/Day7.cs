using System.Text.RegularExpressions;

namespace AdventOfCode._2022
{
    internal class Day7 : Problem
    {
        public override void Solve()
        {
            var root = BuildTree();

            var totalSize = Directory.AllDirectories(root)
                .Select(d => d.ComputeSize())
                .Where(s => s <= 100000)
                .Sum();

            Console.WriteLine(totalSize);
        }

        public override void Solve2()
        {
            var root = BuildTree();
            var requiredSize = 30000000 + root.ComputeSize() - 70000000;

            var candidate = Directory.AllDirectories(root)
                .Select(d => d.ComputeSize())
                .Where(s => s >= requiredSize)
                .Min();

            Console.WriteLine(candidate);
        }

        private Directory BuildTree()
        {
            var root = new Directory("/", null);
            var currentDirectory = root;

            foreach (var instruction in System.IO.File.ReadLines(Input).Skip(1))
            {
                if (instruction == "$ ls" || instruction.StartsWith("dir"))
                {
                    continue;
                }

                if (instruction.StartsWith("$ cd "))
                {
                    var directoryName = instruction.Split(' ', 3).Last();
                    currentDirectory = currentDirectory.GetDirectory(directoryName);
                    continue;
                }

                var (size, fileName) = Regex.Match(instruction, @"(\d+) (.+)").As<long, string>();
                currentDirectory.AddFile(fileName, size);
            }

            return root;
        }

        private abstract class Entry
        {
            public abstract long ComputeSize();
        }

        private class File : Entry
        {
            public File(long size)
            {
                Size = size;
            }

            public long Size { get; }

            public override long ComputeSize() => Size;
        }

        private class Directory : Entry
        {
            public Directory(string name, Directory? parent)
            {
                Parent = parent;
            }

            public Dictionary<string, File> Files { get; } = new();

            public Dictionary<string, Directory> Directories { get; } = new();

            public Directory? Parent { get; }

            public override long ComputeSize() => Directories.Values.Concat<Entry>(Files.Values).Sum(e => e.ComputeSize());

            public Directory GetDirectory(string directoryName)
            {
                if (directoryName == "..")
                {
                    return Parent!;
                }

                if (!Directories.TryGetValue(directoryName, out var directory))
                {
                    directory = new Directory(directoryName, this);
                    Directories.Add(directoryName, directory);
                }

                return directory;
            }

            public void AddFile(string fileName, long size)
            {
                Files[fileName] = new File(size);
            }

            public static IEnumerable<Directory> AllDirectories(Directory root)
            {
                yield return root;

                foreach (var child in root.Directories.Values.SelectMany(AllDirectories))
                {
                    yield return child;
                }
            }
        }
    }
}
