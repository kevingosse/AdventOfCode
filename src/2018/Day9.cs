using System.Diagnostics;

namespace AdventOfCode._2018;

internal class Day9 : Problem
{
    public override void Solve()
    {
        var (nbPlayers, lastMarble) = File.ReadAllText(Input)
            .Match(@"(\d+) players; last marble is worth (\d+) points")
            .As<int, int>();

        Console.WriteLine(ComputeGame(nbPlayers, lastMarble));
    }

    public override void Solve2()
    {
        var (nbPlayers, lastMarble) = File.ReadAllText(Input)
            .Match(@"(\d+) players; last marble is worth (\d+) points")
            .As<int, int>();

        Console.WriteLine(ComputeGame(nbPlayers, lastMarble * 100));
    }

    private static long ComputeGame(int nbPlayers, int lastMarble)
    {
        var scores = new long[nbPlayers];
        var circle = new CircularLinkedList<long>(lastMarble);
        var currentMarble = circle.Root;
        int currentPlayer = 0;

        for (int nextMarble = 1; nextMarble <= lastMarble; nextMarble++)
        {
            if (nextMarble % 23 == 0)
            {
                scores[currentPlayer] += nextMarble;

                var marbleToRemove = circle.ElementAt(currentMarble, -7);
                currentMarble = circle.ElementAt(marbleToRemove, 1);

                scores[currentPlayer] += marbleToRemove.Value;
                circle.Remove(marbleToRemove);
            }
            else
            {
                var position = circle.ElementAt(currentMarble, 1);
                currentMarble = circle.AddAfter(position, nextMarble);
            }

            currentPlayer = (currentPlayer + 1) % nbPlayers;
        }

        return scores.Max();
    }

    private class CircularLinkedList<T>
    {
        private readonly Node[] _storage;
        private readonly Stack<int> _freeList;
        private int _count;

        public CircularLinkedList(int initialSize)
        {
            _storage = new Node[initialSize];
            _freeList = new Stack<int>();
            _count = 1;
            _storage[0] = new();
        }

        public Node Root => _storage[0];

        public Node ElementAt(in Node source, int offset)
        {
            var result = source;
            var absOffset = Math.Abs(offset);

            for (int i = 0; i < absOffset; i++)
            {
                result = _storage[offset > 0 ? result.Next : result.Previous];
            }

            return result;
        }

        public void Remove(in Node node)
        {
            _freeList.Push(node.Index);
            _count--;
            _storage[node.Previous].Next = node.Next;
            _storage[node.Next].Previous = node.Previous;
        }

        public Node AddAfter(in Node source, T value)
        {
            var freeIndex = _freeList.Count > 0 ? _freeList.Pop() : _count;

            _storage[freeIndex] = new Node
            {
                Next = source.Next,
                Previous = source.Index,
                Index = freeIndex,
                Value = value
            };

            _storage[source.Next].Previous = freeIndex;
            _storage[source.Index].Next = freeIndex;

            _count++;

            return _storage[freeIndex];
        }

        [DebuggerDisplay("Index={Index},Previous={Previous},Next={Next},Value={Value}")]
        public struct Node
        {
            public int Previous;
            public int Next;
            public int Index;
            public T Value;
        }
    }
}
