using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2024;

internal class Day17 : Problem
{
    public override void Solve()
    {
        var state = new VmState();

        var input = File.ReadAllLines(Input);

        state.A = Regex.Match(input[0], @"Register A: (\d+)").As<ulong>();
        state.B = Regex.Match(input[1], @"Register B: (\d+)").As<ulong>();
        state.C = Regex.Match(input[2], @"Register C: (\d+)").As<ulong>();

        var program = input[4][9..].Split(',').Select(int.Parse).ToArray();

        var output = Execute(program, state);
        Console.WriteLine(output);
    }

    public override void Solve2()
    {
        /*
            B = A % 8 // Get the first 3 bits
            B = B ^ 5
            C = A / (1 << B)
            B = B ^ 6
            A = A / (1 << 3) // Truncate the first 3 bits
            B = B ^ C
            out B % 8
            jnz 0
        */
        var state = new VmState();

        var input = File.ReadAllLines(Input);
        var program = input[4][9..].Split(',').Select(int.Parse).ToArray();

        var (success, result) = BruteForce(program, state, 0);

        Console.WriteLine(result);
    }

    private (bool, ulong) BruteForce(int[] program, VmState state, int knownDigits)
    {
        if (knownDigits == program.Length)
        {
            return (true, state.A);
        }

        state.A = state.A << 3;
        var expectedOutput = string.Join(',', program.Skip(program.Length - knownDigits - 1));

        for (int i = 0; i <= 7; i++)
        {
            if (Execute(program, state) == expectedOutput)
            {
                // Found a candidate
                var (success, result) = BruteForce(program, state, knownDigits + 1);

                if (success)
                {
                    return (true, result);
                }
            }

            state.A++;
        }

        return (false, default);
    }

    private string Execute(int[] program, VmState state)
    {
        var output = new StringBuilder();

        ulong GetCombo()
        {
            var index = program[state.Ip + 1];

            return index switch
            {
                7 => throw new InvalidOperationException(),
                6 => state.C,
                5 => state.B,
                4 => state.A,
                _ => (ulong)index
            };
        }

        ulong GetLiteral() => (ulong)program[state.Ip + 1];

        while (state.Ip < program.Length)
        {
            var instruction = program[state.Ip];
            bool jumped = false;

            switch (instruction)
            {
                case 0: // adv
                    state.A /= (ulong)Math.Pow(2, GetCombo());
                    break;

                case 1: // bxl
                    state.B ^= GetLiteral();
                    break;

                case 2: // bst
                    state.B = GetCombo() % 8;
                    break;

                case 3: // jnz
                    if (state.A != 0)
                    {
                        state.Ip = (uint)GetLiteral();
                        jumped = true;
                    }
                    break;

                case 4: // bxc
                    state.B ^= state.C;
                    break;

                case 5: // out
                    if (output.Length > 0)
                    {
                        output.Append(',');
                    }

                    output.Append(GetCombo() % 8);
                    break;

                case 6: // bdv
                    state.B = state.A / (ulong)Math.Pow(2, GetCombo());
                    break;

                case 7: // cdv
                    state.C = state.A / (ulong)Math.Pow(2, GetCombo());
                    break;
            }

            if (!jumped)
            {
                state.Ip += 2;
            }
        }

        return output.ToString();
    }

    private struct VmState
    {
        public ulong A;
        public ulong B;
        public ulong C;
        public uint Ip;
    }
}
