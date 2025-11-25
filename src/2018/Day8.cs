namespace AdventOfCode._2018;

internal class Day8 : Problem
{
    public override void Solve()
    {
        var rootNode = ReadNodes(File.ReadAllText(Input));
        Console.WriteLine(rootNode.AsEnumerable().SelectMany(n => n.Metadata).Sum());
    }

    public override void Solve2()
    {
        var rootNode = ReadNodes(File.ReadAllText(Input));
        Console.WriteLine(rootNode.ComputeValue());
    }

    private static Node ReadNodes(string input)
    {
        var values = input.Split(' ').Select(int.Parse).ToArray();

        var rootNode = new Node();
        var currentNode = rootNode;

        int index = 0;

        while (index < values.Length)
        {
            // What state are we in?
            if (currentNode.MetadataCount == 0 && currentNode.ChildNodesCount == 0)
            {
                // Reading header
                currentNode.ChildNodesCount = values[index++];
                currentNode.MetadataCount = values[index++];
                continue;
            }

            if (currentNode.ChildNodesCount > currentNode.ChildNodes.Count)
            {
                // Reading child node
                var childNode = new Node { Parent = currentNode };
                currentNode.ChildNodes.Add(childNode);
                currentNode = childNode;
                continue;
            }

            // Reading metadata
            for (int i = 0; i < currentNode.MetadataCount; i++)
            {
                currentNode.Metadata.Add(values[index++]);
            }

            // Finished reading the current node
            if (currentNode.Parent == null)
            {
                break;
            }

            currentNode = currentNode.Parent;
        }

        return rootNode;
    }

    private class Node
    {
        public int ChildNodesCount { get; set; }
        public int MetadataCount { get; set; }

        public readonly List<int> Metadata = [];
        public readonly List<Node> ChildNodes = [];

        public Node? Parent { get; set; }

        public IEnumerable<Node> AsEnumerable()
        {
            yield return this;

            foreach (var node in ChildNodes.SelectMany(n => n.AsEnumerable()))
            {
                yield return node;
            }
        }

        public long ComputeValue()
        {
            if (ChildNodes.Count == 0)
            {
                return Metadata.Sum();
            }

            long sum = 0;

            foreach (var metadata in Metadata)
            {
                if (metadata == 0 || metadata > ChildNodes.Count)
                {
                    continue;
                }

                sum += ChildNodes[metadata - 1].ComputeValue();
            }
            
            return sum;
        }
    }
}
