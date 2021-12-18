using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    internal class Day12 : Problem
    {
        public override void Solve()
        {
            var rooms = new Dictionary<string, Room>();

            Room GetRoom(string name)
            {
                if (!rooms!.TryGetValue(name, out var room))
                {
                    room = new Room(name);
                    rooms.Add(name, room);
                }

                return room;
            }

            foreach (var line in File.ReadLines(Input))
            {
                var (room1, room2) = line.Split('-').Select(GetRoom);

                room1.Paths.Add(room2);
                room2.Paths.Add(room1);
            }

            var start = GetRoom("start");

            var total = Browse(start, new List<Room>());

            Console.WriteLine(total);
        }

        private int Browse(Room room, List<Room> path)
        {
            if (room.Name == "end")
            {
                return 1;
            }

            var newPath = new List<Room>(path);
            newPath.Add(room);

            int total = 0;

            foreach (var child in room.Paths)
            {
                if (child.IsSmall && path.Contains(child))
                {
                    continue;
                }

                total += Browse(child, newPath);
            }

            return total;
        }

        public override void Solve2()
        {
            var rooms = new Dictionary<string, Room>();

            Room GetRoom(string name)
            {
                if (!rooms!.TryGetValue(name, out var room))
                {
                    room = new Room(name);
                    rooms.Add(name, room);
                }

                return room;
            }

            foreach (var line in File.ReadLines(Input))
            {
                var (room1, room2) = line.Split('-').Select(GetRoom);

                room1.Paths.Add(room2);
                room2.Paths.Add(room1);
            }

            var start = GetRoom("start");

            var total = Browse2(start, new Path());

            Console.WriteLine(total);
        }

        private int Browse2(Room room, Path path)
        {
            if (room.Name == "end")
            {
                return 1;
            }

            var newPath = new Path(path);

            if (room.IsSmall && !newPath.HasPair && newPath.Contains(room))
            {
                newPath.HasPair = true;
            }

            newPath.Add(room);

            int total = 0;

            foreach (var child in room.Paths)
            {
                if (child.IsSmall)
                {
                    if (child.Name == "start")
                    {
                        continue;
                    }

                    if (newPath.HasPair && newPath.Contains(child))
                    {
                        continue;
                    }
                }

                total += Browse2(child, newPath);
            }

            return total;
        }

        private class Path : List<Room>
        {
            public Path()
            {
            }

            public Path(Path path)
                : base(path)
            {
                HasPair = path.HasPair;
            }

            public bool HasPair { get; set; }
        }

        [DebuggerDisplay("{Name}")]
        private class Room : IEquatable<Room?>
        {
            public Room(string name)
            {
                Name = name;
                IsSmall = name.ToLower() == name;
            }

            public string Name { get; }

            public bool IsSmall { get; }

            public List<Room> Paths { get; } = new List<Room>();

            public override bool Equals(object? obj)
            {
                return Equals(obj as Room);
            }

            public bool Equals(Room? other)
            {
                return other != null && Name == other.Name;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Name);
            }
        }
    }
}