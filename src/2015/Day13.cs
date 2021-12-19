using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    internal class Day13 : Problem
    {
        private readonly Dictionary<string, Attendee> _attendees = new();

        public override void Solve()
        {
            foreach (var line in File.ReadLines(Input))
            {
                var match = Regex.Match(line, @"(?<name1>\w+) would (?<verb>\w+) (?<value>[0-9]+) happiness units by sitting next to (?<name2>\w+).");

                var value = int.Parse(match.Groups["value"].Value);

                if (match.Groups["verb"].Value == "lose")
                {
                    value *= -1;
                }

                if (!_attendees.TryGetValue(match.Groups["name1"].Value, out var attendee))
                {
                    attendee = new Attendee(match.Groups["name1"].Value);
                    _attendees.Add(attendee.Name, attendee);
                }

                attendee.Relationship.Add(match.Groups["name2"].Value, value);
            }

            Console.WriteLine(ComputeBestHappiness());
        }

        private int ComputeBestHappiness()
        {
            int maxHappiness = int.MinValue;

            foreach (var permutation in _attendees.Values.GetPermutations())
            {
                int happiness = 0;

                for (int i = 0; i < permutation.Length; i++)
                {
                    Attendee attendee1;
                    Attendee attendee2;

                    if (i == 0)
                    {
                        attendee1 = permutation[0];
                        attendee2 = permutation.Last();
                    }
                    else
                    {
                        attendee1 = permutation[i - 1];
                        attendee2 = permutation[i];
                    }

                    happiness += attendee1.Relationship[attendee2.Name];
                    happiness += attendee2.Relationship[attendee1.Name];
                }

                if (happiness > maxHappiness)
                {
                    maxHappiness = happiness;
                }
            }

            return maxHappiness;
        }

        private record Attendee(string Name)
        {
            public Dictionary<string, int> Relationship { get; } = new Dictionary<string, int>();
        }

        public override void Solve2()
        {
            var myself = new Attendee("Myself");

            foreach (var attendee in _attendees.Values)
            {
                myself.Relationship.Add(attendee.Name, 0);
                attendee.Relationship.Add(myself.Name, 0);
            }

            _attendees.Add(myself.Name, myself);

            Console.WriteLine(ComputeBestHappiness());
        }
    }
}