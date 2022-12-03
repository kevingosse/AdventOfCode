using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2016
{
    internal class Day9 : Problem
    {
        public override void Solve()
        {
            var output = new StringBuilder();

            foreach (var line in File.ReadLines(Input))
            {
                int index = 0;

                var matches = Regex.Matches(line, @"\((?<length>\d+)x(?<count>\d+)\)");

                foreach (Match match in matches)
                {
                    if (match.Index < index)
                    {
                        continue;
                    }

                    output.Append(line[index..match.Index]);
                    index = match.Index;

                    var length = int.Parse(match.Groups["length"].Value);
                    var count = int.Parse(match.Groups["count"].Value);

                    index += match.Length;

                    for (int i = 0; i < count; i++)
                    {
                        output.Append(line.AsSpan().Slice(index, length));
                    }

                    index += length;
                }

                output.Append(line.AsSpan().Slice(index, line.Length - index));
            }

            Console.WriteLine(output.Length);
        }

        public override void Solve2()
        {
            long totalSize = 0;

            foreach (var line in File.ReadLines(Input))
            {
                long index = 0;

                var markers = new Queue<Marker>(Regex.Matches(line, @"\((?<length>\d+)x(?<count>\d+)\)")
                    .Select(m => new Marker(int.Parse(m.Groups["length"].Value), int.Parse(m.Groups["count"].Value), m.Index, m.Value.Length)));

                while (markers.Count > 0)
                {
                    var marker = markers.Peek();

                    totalSize += marker.Index - index;
                    index = marker.Index;

                    totalSize += ComputeExpressionLength(markers);

                    index += marker.Size + marker.Length;
                }

                totalSize += line.Length - index;
            }

            Console.WriteLine(totalSize);
        }

        private record Marker(long Length, long Count, long Index, long Size);

        private static long ComputeExpressionLength(Queue<Marker> markers)
        {
            var marker = markers.Dequeue();
            var length = marker.Length;

            while (markers.Count > 0 && markers.Peek().Index < marker.Index + marker.Length + marker.Size)
            {
                // Next marker is nested
                var nestedMarker = markers.Peek();

                length += ComputeExpressionLength(markers) - nestedMarker.Size - nestedMarker.Length;
            }

            return length * marker.Count;
        }
    }
}
