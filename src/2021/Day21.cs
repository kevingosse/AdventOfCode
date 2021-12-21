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
    internal class Day21 : Problem
    {
        public override void Solve()
        {
            var positions = File.ReadAllLines(Input)
                .Select(l => int.Parse(l.Split(' ').Last()) - 1)
                .ToArray();

            var scores = new int[2];

            int turn = 0;
            int die = 0;
            int rolls = 0;

            while (true)
            {
                int index = turn % 2;

                turn++;

                for (int i = 0; i < 3; i++)
                {
                    rolls++;
                    die++;

                    if (die > 100)
                    {
                        die = 1;
                    }

                    positions[index] = (positions[index] + die) % 10;
                }

                scores[index] += positions[index] + 1;

                if (scores[index] >= 1000)
                {
                    Console.WriteLine(scores[(index + 1) % 2] * rolls);
                    break;
                }
            }

        }

        public override void Solve2()
        {
            var positions = File.ReadAllLines(Input)
                .Select(l => int.Parse(l.Split(' ').Last()) - 1)
                .ToArray();

            var scores = new int[2];

            var results = new List<int>();

            for (int i = 1; i <= 3; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    for (int k = 1; k <= 3; k++)
                    {
                        results.Add(i + j + k);
                    }
                }
            }

            var die = results.GroupBy(v => v).Select(g => (count: g.Count(), value: g.Key)).ToArray();

            var wins = ComputeTurn(0, 0, die, new int[2], positions, new Cache());

            Console.WriteLine(wins.Max());
        }

        private class Cache : Dictionary<(int player, int pos1, int pos2, int score1, int score2), long[]>
        {

        }

        private long[] ComputeTurn(int turn, int player, (int count, int value)[] die, int[] scores, int[] positions, Cache cache)
        {
            if (cache.TryGetValue((player, positions[0], positions[1], scores[0], scores[1]), out var wins))
            {
                return wins;
            }

            wins = new long[2];

            foreach (var (count, value) in die)
            {
                var newPositions = positions.ToArray();
                var newScores = scores.ToArray();

                newPositions[player] = (newPositions[player] + value) % 10;
                newScores[player] += newPositions[player] + 1;

                if (newScores[player] >= 21)
                {
                    wins[player] += count;
                }
                else
                {
                    var newWins = ComputeTurn(turn + 1, player == 0 ? 1 : 0, die, newScores, newPositions, cache);

                    wins[0] += newWins[0] * count;
                    wins[1] += newWins[1] * count;
                }
            }

            cache[(player, positions[0], positions[1], scores[0], scores[1])] = wins;

            return wins;
        }
    }
}