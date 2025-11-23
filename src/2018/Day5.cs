namespace AdventOfCode._2018;

internal class Day5 : Problem
{
    public override void Solve()
    {
        var input = File.ReadAllText(Input);
        var polymer = new LinkedList<char>(input);

        Console.WriteLine(ReducePolymer(polymer));
    }

    public override void Solve2()
    {
        var input = File.ReadAllText(Input);
        var elements = Enumerable.Range(0, 26).Select(i => 'A' + i);
        var result = elements.Select(e => new LinkedList<char>(input.Where(c => char.ToUpper(c) != e)))
            .Select(ReducePolymer)
            .Min();

        Console.WriteLine(result);
    }

    private static int ReducePolymer(LinkedList<char> polymer)
    {
        bool reduced;

        do
        {
            reduced = false;
            var currentNode = polymer.First;

            while (currentNode?.Next != null)
            {
                if (currentNode.Value != currentNode.Next.Value
                    && char.ToUpper(currentNode.Value) == char.ToUpper(currentNode.Next.Value))
                {
                    reduced = true;

                    var nodeToRemove = currentNode;
                    currentNode = currentNode.Previous;

                    polymer.Remove(nodeToRemove.Next);
                    polymer.Remove(nodeToRemove);
                }
                else
                {
                    currentNode = currentNode.Next;
                }
            }
        }
        while (reduced);

        return polymer.Count;
    }
}