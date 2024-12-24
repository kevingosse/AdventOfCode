using System.Text.RegularExpressions;

namespace AdventOfCode._2024;

internal class Day24 : Problem
{
    public override void Solve()
    {
        var gates = LoadGates();
        var result = ComputeResult(gates.Values, 'z');

        Console.WriteLine(result);
    }

    public override void Solve2()
    {
    }

    private ulong ComputeResult(IEnumerable<Gate> gates, char prefix)
    {
        ulong result = 0;

        foreach (var gate in gates.Where(g => g.Name.IsMatch($"{prefix}[0-9]+")))
        {
            if (gate.Value != 0)
            {
                result |= 1UL << int.Parse(gate.Name[1..]);
            }
        }

        return result;
    }

    private Dictionary<string, Gate> LoadGates()
    {
        var gates = new Dictionary<string, Gate>();

        using var reader = File.OpenText(Input);

        string? line;

        while ((line = reader.ReadLine()) is { Length: > 0 })
        {
            var (name, value) = line.Split(": ");
            gates.GetOrAdd(name!, n => new Gate(n)).Value = int.Parse(value!);
        }

        while ((line = reader.ReadLine()) != null)
        {
            var (input1, op, input2, name) = Regex.Match(line, @"(\w+) (\w+) (\w+) -> (\w+)")
                .As<string, string, string, string>();

            ref var gate = ref gates.GetOrAdd(name, n => new Gate(n));
            gate.Operation = Enum.Parse<Gate.Operations>(op);

            gate.Input1 = gates.GetOrAdd(input1, n => new Gate(n));
            gate.Input2 = gates.GetOrAdd(input2, n => new Gate(n));
        }

        return gates;
    }

    private class Gate(string name)
    {
        private int _value;

        public enum Operations
        {
            Constant,
            OR,
            AND,
            XOR
        }

        public string Name { get; } = name;

        public Operations Operation { get; set; }

        public int Value
        {
            get
            {
                return Operation switch
                {
                    Operations.AND => Input1.Value & Input2.Value,
                    Operations.OR => Input1.Value | Input2.Value,
                    Operations.XOR => Input1.Value ^ Input2.Value,
                    Operations.Constant => _value,
                    _ => throw new InvalidOperationException()
                };
            }

            set => _value = value;
        }

        public Gate Input1 { get; set; }
        public Gate Input2 { get; set; }
    }
}
