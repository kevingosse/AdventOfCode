using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AdventOfCode._2017;

internal class Day8 : Problem
{
    public override void Solve()
    {
        var registers = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(Input))
        {
            var match = Regex.Match(line, @"(\w+) (\w+) ([-\d]+) if (\w+) ([!<>=]*) ([-\w]+)");
            var destinationRegister = match.Groups[1].Value;
            var operation = match.Groups[2].Value;
            var operand = int.Parse(match.Groups[3].Value);
            var conditionRegister = match.Groups[4].Value;
            var conditionOperation = match.Groups[5].Value;
            var conditionOperand = int.Parse(match.Groups[6].Value);

            ref var destinationValue = ref CollectionsMarshal.GetValueRefOrAddDefault(registers, destinationRegister, out _);

            if (!CheckCondition(registers, conditionRegister, conditionOperation, conditionOperand))
            {
                continue;
            }

            ExecuteOperation(registers, destinationRegister, operation, operand);
        }

        Console.WriteLine(registers.Values.Max());
    }

    public override void Solve2()
    {
        var registers = new Dictionary<string, int>();
        int max = 0;

        foreach (var line in File.ReadLines(Input))
        {
            var match = Regex.Match(line, @"(\w+) (\w+) ([-\d]+) if (\w+) ([!<>=]*) ([-\w]+)");
            var destinationRegister = match.Groups[1].Value;
            var operation = match.Groups[2].Value;
            var operand = int.Parse(match.Groups[3].Value);
            var conditionRegister = match.Groups[4].Value;
            var conditionOperation = match.Groups[5].Value;
            var conditionOperand = int.Parse(match.Groups[6].Value);

            ref var destinationValue = ref CollectionsMarshal.GetValueRefOrAddDefault(registers, destinationRegister, out _);

            if (!CheckCondition(registers, conditionRegister, conditionOperation, conditionOperand))
            {
                continue;
            }

            ExecuteOperation(registers, destinationRegister, operation, operand);
            
            var finalValue = registers[destinationRegister];

            if (finalValue > max)
            {
                max = finalValue;
            }
        }

        Console.WriteLine(max);
    }

    private void ExecuteOperation(Dictionary<string, int> registers, string register, string operation, int operand)
    {
        ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(registers, register, out _);

        if (operation == "inc")
        {
            value += operand;
        }
        else if (operation == "dec")
        {
            value -= operand;
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    private bool CheckCondition(Dictionary<string, int> registers, string register, string operation, int operand)
    {
        var value = CollectionsMarshal.GetValueRefOrAddDefault(registers, register, out _);

        return operation switch
        {
            "==" => value == operand,
            "!=" => value != operand,
            "<" => value < operand,
            ">" => value > operand,
            "<=" => value <= operand,
            ">=" => value >= operand,
            _ => throw new InvalidOperationException()
        };
    }
}
