using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    internal class Day7 : Problem
    {
        public override void Solve()
        {
            var gates = new Dictionary<string, Gate>();

            Gate GetOrCreate(string name)
            {
                if (!gates!.TryGetValue(name, out var gate))
                {
                    gate = new Gate();
                    gates[name] = gate;
                }

                return gate;
            }

            foreach (var line in File.ReadLines(Input))
            {
                // af AND ah -> ai
                var match = Regex.Match(line, @"((?<input1>[a-z0-9]*) )?(?<verb>[A-Z]*) ?(?<input2>[a-z0-9]+) -> (?<output>[a-z]+)");

                var verb = match.Groups["verb"].Value;
                var gate = GetOrCreate(match.Groups["output"].Value);

                if (verb == "NOT")
                {
                    gate.Type = GateType.Not;
                    gate.Input1 = GetOrCreate(match.Groups["input2"].Value);
                }
                else if (verb == "AND")
                {
                    gate.Type = GateType.And;
                    gate.Input1 = GetOrCreate(match.Groups["input1"].Value);
                    gate.Input2 = GetOrCreate(match.Groups["input2"].Value);
                }
                else if (verb == "OR")
                {
                    gate.Type = GateType.Or;
                    gate.Input1 = GetOrCreate(match.Groups["input1"].Value);
                    gate.Input2 = GetOrCreate(match.Groups["input2"].Value);
                }
                else if (verb == "LSHIFT")
                {
                    gate.Type = GateType.LShift;
                    gate.Input1 = GetOrCreate(match.Groups["input1"].Value);
                    gate.Input2 = new Gate(ushort.Parse(match.Groups["input2"].Value));
                }
                else if (verb == "RSHIFT")
                {
                    gate.Type = GateType.RShift;
                    gate.Input1 = GetOrCreate(match.Groups["input1"].Value);
                    gate.Input2 = new Gate(ushort.Parse(match.Groups["input2"].Value));
                }
                else
                {
                    if (ushort.TryParse(match.Groups["input2"].Value, out var input))
                    {
                        gate.Type = GateType.Constant;
                        gate.Value = input;
                    }
                    else
                    {
                        gate.Type = GateType.Passthrough;
                        gate.Input1 = GetOrCreate(match.Groups["input2"].Value);
                    }
                }
            }

            foreach (var kvp in gates)
            {
                if (kvp.Value.Type == GateType.None)
                {
                    kvp.Value.Type = GateType.Constant;
                    kvp.Value.Value = ushort.Parse(kvp.Key);
                }
            }

            var resultGate = gates["a"];

            resultGate.Evaluate();

            Console.WriteLine(resultGate.Value);
        }

        private class Gate
        {
            public Gate()
            {
            }

            public Gate(ushort value)
            {
                Type = GateType.Constant;
                Value = value;
            }

            public GateType Type { get; set; }

            public Gate Input1 { get; set; }
            public Gate Input2 { get; set; }

            public ushort? Value { get; set; }

            public void Evaluate()
            {
                if (Input1 != null && Input1.Value == null)
                {
                    Input1.Evaluate();
                }

                if (Input2 != null && Input2.Value == null)
                {
                    Input2.Evaluate();
                }

                switch (Type)
                {
                    case GateType.And:
                        Value = (ushort)(Input1.Value.Value & Input2.Value.Value);
                        break;

                    case GateType.Or:
                        Value = (ushort)(Input1.Value.Value | Input2.Value.Value);
                        break;

                    case GateType.Not:
                        unchecked
                        {
                            Value = (ushort)~Input1.Value.Value;
                        }

                        break;

                    case GateType.LShift:
                        Value = (ushort)(Input1.Value.Value << Input2.Value.Value);
                        break;

                    case GateType.RShift:
                        Value = (ushort)(Input1.Value.Value >> Input2.Value.Value);
                        break;

                    case GateType.Passthrough:
                        Value = Input1.Value.Value;
                        break;
                }
            }
        }

        private enum GateType
        {
            None,
            Constant,
            Not,
            And,
            Or,
            LShift,
            RShift,
            Passthrough
        }

        public override void Solve2()
        {
            var gates = new Dictionary<string, Gate>();

            Gate GetOrCreate(string name)
            {
                if (!gates!.TryGetValue(name, out var gate))
                {
                    gate = new Gate();
                    gates[name] = gate;
                }

                return gate;
            }

            foreach (var line in File.ReadLines(Input))
            {
                // af AND ah -> ai
                var match = Regex.Match(line, @"((?<input1>[a-z0-9]*) )?(?<verb>[A-Z]*) ?(?<input2>[a-z0-9]+) -> (?<output>[a-z]+)");

                var verb = match.Groups["verb"].Value;
                var gate = GetOrCreate(match.Groups["output"].Value);

                if (verb == "NOT")
                {
                    gate.Type = GateType.Not;
                    gate.Input1 = GetOrCreate(match.Groups["input2"].Value);
                }
                else if (verb == "AND")
                {
                    gate.Type = GateType.And;
                    gate.Input1 = GetOrCreate(match.Groups["input1"].Value);
                    gate.Input2 = GetOrCreate(match.Groups["input2"].Value);
                }
                else if (verb == "OR")
                {
                    gate.Type = GateType.Or;
                    gate.Input1 = GetOrCreate(match.Groups["input1"].Value);
                    gate.Input2 = GetOrCreate(match.Groups["input2"].Value);
                }
                else if (verb == "LSHIFT")
                {
                    gate.Type = GateType.LShift;
                    gate.Input1 = GetOrCreate(match.Groups["input1"].Value);
                    gate.Input2 = new Gate(ushort.Parse(match.Groups["input2"].Value));
                }
                else if (verb == "RSHIFT")
                {
                    gate.Type = GateType.RShift;
                    gate.Input1 = GetOrCreate(match.Groups["input1"].Value);
                    gate.Input2 = new Gate(ushort.Parse(match.Groups["input2"].Value));
                }
                else
                {
                    if (ushort.TryParse(match.Groups["input2"].Value, out var input))
                    {
                        gate.Type = GateType.Constant;
                        gate.Value = input;
                    }
                    else
                    {
                        gate.Type = GateType.Passthrough;
                        gate.Input1 = GetOrCreate(match.Groups["input2"].Value);
                    }
                }
            }

            foreach (var kvp in gates)
            {
                if (kvp.Value.Type == GateType.None)
                {
                    kvp.Value.Type = GateType.Constant;
                    kvp.Value.Value = ushort.Parse(kvp.Key);
                }
            }

            var resultGate = gates["a"];

            resultGate.Evaluate();

            var value = resultGate.Value;

            foreach (var gate in gates.Values)
            {
                if (gate.Type != GateType.Constant)
                {
                    gate.Value = null;
                }
            }

            var bGate = gates["b"];
            bGate.Type = GateType.Constant;
            bGate.Value = value;

            resultGate.Evaluate();

            Console.WriteLine(resultGate.Value);
        }
    }
}