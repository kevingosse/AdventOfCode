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
    internal class Day9 : Problem
    {
        public override void Solve()
        {
            var cities = new Dictionary<string, City>();

            City GetCity(string name)
            {
                if (!cities!.TryGetValue(name, out var city))
                {
                    city = new City(name);
                    cities.Add(name, city);
                }

                return city;
            }

            foreach (var line in File.ReadLines(Input))
            {
                var match = Regex.Match(line, @"(?<city1>\w+) to (?<city2>\w+) = (?<distance>[0-9]+)");

                var city1 = GetCity(match.Groups["city1"].Value);
                var city2 = GetCity(match.Groups["city2"].Value);
                var distance = int.Parse(match.Groups["distance"].Value);

                city1.Links.Add((city2, distance));
                city2.Links.Add((city1, distance));
            }

            int? min = null;

            foreach (var start in cities.Values)
            {
                var result = Browse(start, new(), 0, cities.Values.Count);

                if (result == null)
                {
                    continue;
                }

                if (min == null || result < min.Value)
                {
                    min = result;
                }
            }

            Console.WriteLine(min);
        }

        private int? Browse(City city, List<City> path, int cumulatedDistance, int count)
        {
            path.Add(city);

            if (path.Count == count)
            {
                return cumulatedDistance;
            }

            int? min = null;

            foreach (var nextCity in city.Links)
            {
                if (path.Contains(nextCity.city))
                {
                    continue;
                }

                var distance = Browse(nextCity.city, new(path), cumulatedDistance + nextCity.distance, count);

                if (distance != null)
                {
                    if (min == null || distance.Value < min.Value)
                    {
                        min = distance;
                    }
                }
            }

            return min;
        }

        public class City
        {
            public City(string name)
            {
                Name = name;
            }

            public string Name { get; }

            public List<(City city, int distance)> Links { get; } = new();

            public override bool Equals(object? obj)
            {
                return obj is City city && Name == city.Name;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Name);
            }

            public override string ToString() => Name;
        }

        public override void Solve2()
        {
            var cities = new Dictionary<string, City>();

            City GetCity(string name)
            {
                if (!cities!.TryGetValue(name, out var city))
                {
                    city = new City(name);
                    cities.Add(name, city);
                }

                return city;
            }

            foreach (var line in File.ReadLines(Input))
            {
                var match = Regex.Match(line, @"(?<city1>\w+) to (?<city2>\w+) = (?<distance>[0-9]+)");

                var city1 = GetCity(match.Groups["city1"].Value);
                var city2 = GetCity(match.Groups["city2"].Value);
                var distance = int.Parse(match.Groups["distance"].Value);

                city1.Links.Add((city2, distance));
                city2.Links.Add((city1, distance));
            }

            int? max = null;

            foreach (var start in cities.Values)
            {
                var result = Browse2(start, new(), 0, cities.Values.Count);

                if (result == null)
                {
                    continue;
                }

                if (max == null || result > max.Value)
                {
                    max = result;
                }
            }

            Console.WriteLine(max);
        }

        private int? Browse2(City city, List<City> path, int cumulatedDistance, int count)
        {
            path.Add(city);

            if (path.Count == count)
            {
                return cumulatedDistance;
            }

            int? max = null;

            foreach (var nextCity in city.Links)
            {
                if (path.Contains(nextCity.city))
                {
                    continue;
                }

                var distance = Browse2(nextCity.city, new(path), cumulatedDistance + nextCity.distance, count);

                if (distance != null)
                {
                    if (max == null || distance.Value > max.Value)
                    {
                        max = distance;
                    }
                }
            }

            return max;
        }

    }
}