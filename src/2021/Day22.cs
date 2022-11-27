using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    internal class Day22 : Problem
    {
        public override void Solve()
        {
            bool IsValid(Point p)
            {
                return p.x >= -50 && p.x <= 50
                    && p.y >= -50 && p.y <= 50
                    && p.z >= -50 && p.z <= 50;
            }

            var instructions = new List<Instruction>();

            foreach (var line in File.ReadLines(Input))
            {
                var match = Regex.Match(line, @"(?<verb>\w+) x=(?<x1>-?[0-9]+)\.\.(?<x2>-?[0-9]+),y=(?<y1>-?[0-9]+)\.\.(?<y2>-?[0-9]+),z=(?<z1>-?[0-9]+)\.\.(?<z2>-?[0-9]+)");

                var p1 = new Point(int.Parse(match.Groups["x1"].Value), int.Parse(match.Groups["y1"].Value), int.Parse(match.Groups["z1"].Value));
                var p2 = new Point(int.Parse(match.Groups["x2"].Value), int.Parse(match.Groups["y2"].Value), int.Parse(match.Groups["z2"].Value));

                if (!IsValid(p1) || !IsValid(p2))
                {
                    continue;
                }

                instructions.Add(new Instruction(match.Groups["verb"].Value == "on", p1, p2));
            }

            var map = new bool[101, 101, 101];

            foreach (var instruction in instructions)
            {
                for (int x = instruction.p1.x; x <= instruction.p2.x; x++)
                {
                    for (int y = instruction.p1.y; y <= instruction.p2.y; y++)
                    {
                        for (int z = instruction.p1.z; z <= instruction.p2.z; z++)
                        {
                            map[x + 50, y + 50, z + 50] = instruction.turnOn;
                        }
                    }
                }
            }

            int count = 0;

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    for (int z = 0; z < map.GetLength(2); z++)
                    {
                        if (map[x, y, z])
                        {
                            count++;
                        }
                    }
                }
            }

            Console.WriteLine(count);
        }

        private record Point(int x, int y, int z);

        private record Instruction(bool turnOn, Point p1, Point p2);

        public override void Solve2()
        {
            var instructions = new List<Instruction>();

            foreach (var line in File.ReadLines(Input))
            {
                var match = Regex.Match(line, @"(?<verb>\w+) x=(?<x1>-?[0-9]+)\.\.(?<x2>-?[0-9]+),y=(?<y1>-?[0-9]+)\.\.(?<y2>-?[0-9]+),z=(?<z1>-?[0-9]+)\.\.(?<z2>-?[0-9]+)");

                instructions.Add(new Instruction(
                    match.Groups["verb"].Value == "on",
                    new Point(int.Parse(match.Groups["x1"].Value), int.Parse(match.Groups["y1"].Value), int.Parse(match.Groups["z1"].Value)),
                    new Point(int.Parse(match.Groups["x2"].Value), int.Parse(match.Groups["y2"].Value), int.Parse(match.Groups["z2"].Value))));
            }

            var map = new List<Cube>();

            foreach (var instruction in instructions)
            {
                var cube = new Cube(instruction.p1, instruction.p2);

                for (int i = 0; i < map.Count; i++)
                {
                    if (!Intersect(cube, map[i]))
                    {
                        continue;
                    }


                }
            }

        }

        private List<Cube>? Intersection(Cube cube1, Cube cube2)
        {
            var minX = Math.Max(cube1.p1.x, cube2.p1.x);
            var maxX = Math.Min(cube1.p2.x, cube2.p2.x);
            var minY = Math.Max(cube1.p1.y, cube2.p1.y);
            var maxY = Math.Min(cube1.p2.y, cube2.p2.y);
            var minZ = Math.Max(cube1.p1.z, cube2.p1.z);
            var maxZ = Math.Min(cube1.p2.z, cube2.p2.z);

            if (minX > maxX || minY > maxY || minZ > maxZ)
            {
                return null;
            }

            var result = new List<Cube>();

            if (cube2.p1.x < minX)
            {
                result.Add(new Cube(new Point(cube2.p1.x, cube2.p1.y, cube2.p1.z), new Point(minX - 1, cube2.p2.y, cube2.p2.z)));
            }

            if (maxX < cube2.p2.x)
            {
                result.Add(new Cube(new Point(maxX + 1, cube2.p1.y, cube2.p1.z), new Point(cube2.p2.x, cube2.p2.y, cube2.p2.z)));
            }

            if (cube2.p1.y < minY)
            {
                result.Add(new Cube(new Point(minX, cube2.p1.y, cube2.p1.z), new Point(maxX, minY - 1, cube2.p2.z)));
            }

            if (maxY < cube2.p2.y)
            {
                result.Add(new Cube(new Point(minX, maxY + 1, cube2.p1.z), new Point(maxX, cube2.p2.y, cube2.p2.z)));
            }

            if (cube2.p1.z < minZ)
            {
                result.Add(new Cube(new Point(minX, minY, cube2.p1.z), new Point(maxX, maxY, minZ - 1)));
            }

            if (maxZ < cube2.p2.z)
            {
                result.Add(new Cube(new Point(minX, minY, maxZ + 1), new Point(maxX, maxY, cube2.p2.z)));
            }

            return result;
        }

        private Cube[] Add(Cube cube1, Cube cube2)
        {
            return null;
        }

        private Cube[] Substract(Cube cube1, Cube cube2)
        {
            return null;
        }

        private bool Intersect(Cube cube1, Cube cube2)
        {
            return !(cube1.p2.x < cube2.p1.x
                || cube2.p2.x < cube1.p1.x
                || cube1.p2.z < cube2.p1.z
                || cube2.p2.z < cube1.p1.z
                || cube1.p2.y < cube2.p1.y
                || cube2.p2.y < cube1.p1.y);
        }

        private record Cube(Point p1, Point p2);
    }
}