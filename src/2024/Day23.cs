using System.Collections.Immutable;
using System.Runtime.InteropServices;

namespace AdventOfCode._2024;

internal class Day23 : Problem
{
    public override void Solve()
    {
        var nodes = LoadNodes();
        var knownSubnets = new HashSet<string>();
        long result = 0;

        foreach (var node in nodes.Values)
        {
            foreach (var subnet in node.GetSubnet(3))
            {
                var hash = string.Join(',', subnet.Select(n => n.Name).Order());

                if (!knownSubnets.Add(hash))
                {
                    continue;
                }

                if (subnet.Any(n => n.Name.StartsWith('t')))
                {
                    result++;
                }
            }
        }

        Console.WriteLine(result);
    }

    private Dictionary<string, Node> LoadNodes()
    {
        var nodes = new Dictionary<string, Node>();

        foreach (var line in File.ReadLines(Input))
        {
            var (name1, name2) = line.Split('-');

            ref var node1 = ref CollectionsMarshal.GetValueRefOrAddDefault(nodes, name1!, out var exists);

            if (!exists)
            {
                node1 = new(name1!);
            }

            ref var node2 = ref CollectionsMarshal.GetValueRefOrAddDefault(nodes, name2!, out exists);

            if (!exists)
            {
                node2 = new(name2!);
            }

            node1!.Neighbors.Add(node2!.Name, node2!);
            node2!.Neighbors.Add(node1!.Name, node1!);
        }

        return nodes;
    }

    public override void Solve2()
    {
        var nodes = LoadNodes();
        ImmutableHashSet<Node> biggestSubnet = [];

        foreach (var node in nodes.Values)
        {
            foreach (var subnet in node.GetInterconnectedSubnets())
            {
                if (subnet.Count > biggestSubnet.Count)
                {
                    biggestSubnet = subnet;
                }
            }
        }

        Console.WriteLine(string.Join(',', biggestSubnet.Select(n => n.Name).Order()));
    }

    private class Node(string name)
    {
        public string Name { get; } = name;
        public Dictionary<string, Node> Neighbors { get; } = new();

        public IEnumerable<ImmutableHashSet<Node>> GetSubnet(int size)
        {
            var queue = new Stack<(Node node, int depth, ImmutableHashSet<Node> subnet)>();

            queue.Push((this, 0, []));

            while (queue.Count > 0)
            {
                var (node, depth, subnet) = queue.Pop();

                if (depth == size)
                {
                    if (node == this)
                    {
                        yield return subnet;
                    }

                    continue;
                }

                if (subnet.Contains(node))
                {
                    continue;
                }

                subnet = subnet.Add(node);

                foreach (var neighbor in node.Neighbors.Values)
                {
                    queue.Push((neighbor, depth + 1, subnet));
                }
            }
        }

        public IEnumerable<ImmutableHashSet<Node>> GetInterconnectedSubnets()
        {
            var queue = new Stack<(Node node, ImmutableHashSet<Node> subnet)>();
            queue.Push((this, []));

            while (queue.Count > 0)
            {
                var (node, subnet) = queue.Pop();
                bool isInterconnected = subnet.All(n => node.Neighbors.ContainsKey(n.Name));

                if (!isInterconnected)
                {
                    continue;
                }

                subnet = subnet.Add(node);
                yield return subnet;

                foreach (var neighbor in node.Neighbors.Values)
                {
                    if (neighbor.Name.CompareTo(node.Name) > 0
                        && !subnet.Contains(neighbor))
                    {
                        queue.Push((neighbor, subnet));
                    }
                }
            }
        }
    }
}
