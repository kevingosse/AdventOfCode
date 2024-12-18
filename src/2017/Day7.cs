using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AdventOfCode._2017;

internal class Day7 : Problem
{
    public override void Solve()
    {
        var nodes = BuildTree();

        var root = nodes.Values.Single(n => nodes.Values.All(c => !c.Children.Contains(n)));
        Console.WriteLine(root.Name);
    }

    public override void Solve2()
    {
        var nodes = BuildTree();
        var root = nodes.Values.Single(n => nodes.Values.All(c => !c.Children.Contains(n)));

        Console.WriteLine(root.GetUnbalancedWeight());
    }

    private Dictionary<string, Node> BuildTree()
    {
        var nodes = new Dictionary<string, Node>();

        foreach (var line in File.ReadLines(Input))
        {
            var (name, weight) = Regex.Match(line, @"(\w+) \((\d+)\)").As<string, int>();

            ref var node = ref CollectionsMarshal.GetValueRefOrAddDefault(nodes, name, out var exists);

            if (!exists)
            {
                node = new(name);
            }

            node!.Weight = weight;

            var index = line.IndexOf("->");

            if (index != -1)
            {
                foreach (var child in line.Substring(index + 3).Split(',').Select(c => c.Trim()))
                {
                    ref var childNode = ref CollectionsMarshal.GetValueRefOrAddDefault(nodes, child, out exists);

                    if (!exists)
                    {
                        childNode = new(child);
                    }

                    node.Children.Add(childNode!);
                }
            }
        }

        return nodes;
    }

    private class Node(string name)
    {
        public string Name { get; set; } = name;
        public int Weight { get; set; }
        public List<Node> Children { get; set; } = new();

        public int ComputeBranchWeight() => Weight + Children.Sum(c => c.ComputeBranchWeight());

        public int GetUnbalancedWeight()
        {
            var branches = Children
                .GroupBy(c => c.ComputeBranchWeight())
                .OrderBy(g => g.Count())
                .ToList();

            if (branches.Count is 0 or 1)
            {
                // Balanced
                return 0;
            }

            var (unbalancedBranch, referenceBranch) = branches;

            var unbalancedNode = unbalancedBranch!.Single();
            var unbalancedChild = unbalancedNode.Children
                .Select(c => c.GetUnbalancedWeight())
                .Where(w => w != 0)
                .DefaultIfEmpty(referenceBranch!.Key - unbalancedBranch!.Key + unbalancedNode.Weight)
                .FirstOrDefault(w => w != 0);

            return unbalancedChild;
        }
    }
}
