using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    internal class Day19 : Problem
    {
        private Transformation[,]? _transformations;
        private List<Scanner>? _scanners;

        public override void Solve()
        {
            _scanners = new List<Scanner>();

            foreach (var line in File.ReadLines(Input))
            {
                if (line.StartsWith("---"))
                {
                    _scanners.Add(new Scanner { Name = line });
                    continue;
                }

                if (line.Length == 0)
                {
                    continue;
                }

                var (x, y, z) = line.Split(',').Select(int.Parse);
                _scanners.Last().Add(new Point(x, y, z));
            }

            _transformations = ExtractTransformations(_scanners);

            var beacons = new HashSet<Point>(_scanners[0]);

            for (int i = 1; i < _scanners.Count; i++)
            {
                var transformation = _transformations[i, 0];

                foreach (var point in _scanners[i])
                {
                    beacons.Add(transformation.Apply(point));
                }
            }

            Console.WriteLine($"Found {beacons.Count} beacons");
        }

        private static Transformation[,] ExtractTransformations(List<Scanner> scanners)
        {
            var transformations = new Transformation[scanners.Count, scanners.Count];

            for (int i = 0; i < scanners.Count; i++)
            {
                var scanner1 = scanners[i];

                for (int j = 0; j < scanners.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    var scanner2 = scanners[j];

                    foreach (var rotation in Rotations)
                    {
                        var distCounts = new Dictionary<Point, int>();

                        foreach (var beacon1 in scanner1)
                        {
                            var point1 = rotation(beacon1);

                            foreach (var beacon2 in scanner2)
                            {
                                var point2 = beacon2;

                                var offset = point2 - point1;

                                ref var dist = ref CollectionsMarshal.GetValueRefOrAddDefault(distCounts, offset, out _);

                                dist++;

                                if (dist == 12)
                                {
                                    var transformation = new Transformation();
                                    transformation.Steps.Add((rotation, offset));
                                    transformations[i, j] = transformation;
                                }
                            }
                        }
                    }
                }
            }

            bool TransformationsComplete()
            {
                for (int i = 0; i < scanners.Count; i++)
                {
                    if (transformations[i, 0] == null)
                    {
                        return false;
                    }
                }

                return true;
            }

            while (!TransformationsComplete())
            {
                for (int i = 0; i < scanners.Count; i++)
                {
                    if (transformations[i, 0] != null)
                    {
                        continue;
                    }

                    for (int j = 0; j < scanners.Count; j++)
                    {
                        if (transformations[j, 0] == null)
                        {
                            continue;
                        }

                        if (transformations[i, j] == null)
                        {
                            continue;
                        }

                        var newTransformation = transformations[i, j].Clone();
                        newTransformation.Steps.AddRange(transformations[j, 0].Steps);
                        transformations[i, 0] = newTransformation;
                    }
                }
            }

            return transformations;
        }

        private class Transformation
        {
            public List<(Func<Point, Point> rotation, Point offset)> Steps { get; init; } = new();

            public Transformation Clone()
            {
                return new Transformation { Steps = Steps.ToList() };
            }

            public Point Apply(Point origin)
            {
                foreach (var step in Steps)
                {
                    origin = step.rotation(origin) + step.offset;
                }

                return origin;
            }
        }

        private static readonly Func<Point, Point>[] Rotations =
        {
            p => new Point(p.X, p.Y, p.Z),
            p => new Point(p.Y, p.Z, p.X),
            p => new Point(p.Z, p.X, p.Y),
            p => new Point(-p.X, p.Z, p.Y),
            p => new Point(p.Z, p.Y, -p.X),
            p => new Point(p.Y, -p.X, p.Z),
            p => new Point(p.X, p.Z, -p.Y),
            p => new Point(p.Z, -p.Y, p.X),
            p => new Point(-p.Y, p.X, p.Z),
            p => new Point(p.X, -p.Z, p.Y),
            p => new Point(-p.Z, p.Y, p.X),
            p => new Point(p.Y, p.X, -p.Z),
            p => new Point(-p.X, -p.Y, p.Z),
            p => new Point(-p.Y, p.Z, -p.X),
            p => new Point(p.Z, -p.X, -p.Y),
            p => new Point(-p.X, p.Y, -p.Z),
            p => new Point(p.Y, -p.Z, -p.X),
            p => new Point(-p.Z, -p.X, p.Y),
            p => new Point(p.X, -p.Y, -p.Z),
            p => new Point(-p.Y, -p.Z, p.X),
            p => new Point(-p.Z, p.X, -p.Y),
            p => new Point(-p.X, -p.Z, -p.Y),
            p => new Point(-p.Z, -p.Y, -p.X),
            p => new Point(-p.Y, -p.X, -p.Z)
        };

        private class Scanner : List<Point>
        {
            public string Name { get; init; }
        }

        private record Point(int X, int Y, int Z)
        {
            public override string ToString() => $"{X},{Y},{Z}";

            public static Point operator +(Point p1, Point p2)
            {
                return new Point(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
            }

            public static Point operator -(Point p1, Point p2)
            {
                return new Point(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
            }
        }

        public override void Solve2()
        {
            if (_transformations == null)
            {
                throw new InvalidOperationException("Need to solve 1 first");
            }

            var positions = new List<Point> { new Point(0, 0, 0) };

            for (int i = 1; i < _scanners.Count; i++)
            {
                positions.Add(_transformations[i, 0].Apply(new Point(0, 0, 0)));
            }

            int maxDistance = 0;

            for (int i = 0; i < positions.Count; i++)
            {
                for (int j = 0; j < positions.Count; j++)
                {
                    var diff = positions[j] - positions[i];
                    var distance = Math.Abs(diff.X) + Math.Abs(diff.Y) + Math.Abs(diff.Z);

                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                    }
                }
            }

            Console.WriteLine($"Distance max: {maxDistance}");
        }
    }
}