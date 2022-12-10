namespace AdventOfCode._2016;

internal class Day12 : Problem
{
    public override void Solve()
    {
        var instructions = File.ReadAllLines(Input).Select(i => i.Split(' ')).ToArray();

        var registers = new int[4];
        int ip = 0;

        ref int GetRegister(string operand) => ref registers[operand[0] - 'a'];

        while (ip < instructions.Length)
        {
            var (instruction, operand1, operand2) = instructions[ip];

            if (instruction == "jnz")
            {
                var value = char.IsDigit(operand1![0]) ? int.Parse(operand1) : GetRegister(operand1);
                ip += value == 0 ? 1 : int.Parse(operand2!);
                continue;
            }

            if (instruction == "cpy")
            {
                GetRegister(operand2!) = char.IsDigit(operand1![0]) ? int.Parse(operand1) : GetRegister(operand1);
            }
            else if (instruction == "inc")
            {
                GetRegister(operand1!) += 1;
            }
            else if (instruction == "dec")
            {
                GetRegister(operand1!) -= 1;
            }

            ip += 1;
        }

        Console.WriteLine(GetRegister("a"));
    }

    public override void Solve2()
    {
        var registers = new[] { 0, 0, 1, 0 };

        var instructions = File.ReadAllLines(Input)
            .Select(i =>
            {
                var (instruction, operand1, operand2) = i.Split(' ');
                return new Instruction(instruction!, operand1, operand2, registers);
            })
            .ToArray();

        int ip = 0;

        while (ip < instructions.Length)
        {
            instructions[ip].Execute(ref ip);
        }

        Console.WriteLine(registers[0]);
    }

    private readonly struct Source
    {
        private readonly int[]? _registers;
        private readonly int _value;

        public Source(string operand, int[] registers)
        {
            if (int.TryParse(operand, out var value))
            {
                _value = value;
            }
            else
            {
                _registers = registers;
                _value = operand[0] - 'a';
            }
        }

        public int Read() => _registers != null ? _registers[_value] : _value;

        public void Write(int value) => _registers![_value] = value;
    }

    private class Instruction
    {
        private readonly Source _source1;
        private readonly Source _source2;

        private readonly Func<int, int> _execute;

        public Instruction(string op, string? operand1, string? operand2, int[] registers)
        {
            if (operand1 != null)
            {
                _source1 = new Source(operand1, registers);
            }

            if (operand2 != null)
            {
                _source2 = new Source(operand2, registers);
            }
            
            if (op == "jnz")
            {
                _execute = ip => ip + (_source1.Read() == 0 ? 1 : _source2.Read());
            }
            else if (op == "cpy")
            {
                _execute = ip =>
                {
                    _source2.Write(_source1.Read());
                    return ip + 1;
                };
            }
            else if (op == "inc")
            {
                _execute = ip =>
                {
                    _source1.Write(_source1.Read() + 1);
                    return ip + 1;
                };
            }
            else if (op == "dec")
            {
                _execute = ip =>
                {
                    _source1.Write(_source1.Read() - 1);
                    return ip + 1;
                };
            }
            else
            {
                throw new ArgumentException("Invalid operation: " + op);
            }
        }

        public void Execute(ref int ip) => ip = _execute(ip);
    }
}