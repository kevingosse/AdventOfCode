using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2016
{
    internal class Day4 : Problem
    {
        public override void Solve()
        {
            long total = 0;

            foreach (var line in File.ReadLines(Input))
            {
                var match = Regex.Match(line, @"(?<name>[a-z\-]+)-(?<id>[0-9]+)\[(?<checksum>[a-z]+)\]");

                var name = match.Groups["name"].Value;
                var id = int.Parse(match.Groups["id"].Value);
                var checksum = match.Groups["checksum"].Value;

                var letters = name.Replace("-", "")
                    .GroupBy(c => c)
                    .Select(g => (letter: g.Key, count: g.Count()))
                    .OrderByDescending(g => g.count)
                    .ThenBy(g => g.letter)
                    .Select(l => l.letter);

                if (string.Concat(letters.Take(5)) == checksum)
                {
                    total += id;
                }
            }

            Console.WriteLine(total);
        }

        public override void Solve2()
        {
            foreach (var line in File.ReadLines(Input))
            {
                var match = Regex.Match(line, @"(?<name>[a-z\-]+)-(?<id>[0-9]+)\[(?<checksum>[a-z]+)\]");

                var name = match.Groups["name"].Value;
                var id = int.Parse(match.Groups["id"].Value);

                int offset = id % 26;

                var decryptedName = new StringBuilder();

                foreach (var letter in name)
                {
                    if (letter == '-')
                    {
                        decryptedName.Append(letter);
                        continue;
                    }

                    char newLetter = (char)(letter + offset);

                    if (newLetter > 'z')
                    {
                        newLetter = (char)(newLetter - 26);
                    }

                    decryptedName.Append(newLetter);
                }

                if (decryptedName.ToString() == "northpole-object-storage")
                {
                    Console.WriteLine(id);
                }
            }
        }
    }
}
