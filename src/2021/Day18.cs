using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    internal class Day18 : Problem
    {
        public override void Solve()
        {
            var numbers = File.ReadLines(Input).Select(Number.Parse).ToArray();

            var result = numbers.Aggregate((n1, n2) => n1 + n2);

            Console.WriteLine(result.Magnitude());
        }

        private class Number
        {
            [MemberNotNullWhen(true, nameof(Left), nameof(Right))]
            public bool IsPair => Left != null;

            public bool IsLeaf => !IsPair || (!Left.IsPair && !Right.IsPair);

            public int Value { get; set; }

            public Number? Parent { get; set; }

            public Number? Left { get; set; }

            public Number? Right { get; set; }

            public static Number operator +(Number number1, Number number2)
            {
                var number = new Number
                {
                    Left = number1,
                    Right = number2
                };

                number1.Parent = number;
                number2.Parent = number;

                while (true)
                {
                    if (number.Explode())
                    {
                        continue;
                    }

                    if (number.Split())
                    {
                        continue;
                    }

                    return number;
                }
            }

            public static Number Parse(string input)
            {
                var stack = new Stack<Number>();

                for (int i = 0; i < input.Length; i++)
                {
                    var c = input[i];

                    if (c == '[')
                    {
                        stack.Push(new Number { Parent = stack.Count > 0 ? stack.Peek() : null });
                    }
                    else if (c == ',')
                    {
                        continue;
                    }
                    else if (c == ']')
                    {
                        var value = stack.Pop();

                        if (stack.Count == 0)
                        {
                            return value;
                        }
                        else
                        {
                            stack.Peek().Assign(value);
                        }

                    }
                    else
                    {
                        stack.Peek().Assign(new Number { Value = c - '0', Parent = stack.Count > 0 ? stack.Peek() : null });
                    }
                }

                throw new InvalidOperationException("Unbalanced stack");
            }

            public bool Split()
            {
                foreach (var (number, _) in Browse())
                {
                    if (!number.IsPair && number.Value > 9)
                    {
                        number.Left = new Number { Parent = number, Value = number.Value / 2 };
                        number.Right = new Number { Parent = number, Value = (int)Math.Ceiling(number.Value / 2.0) };

                        return true;
                    }
                }

                return false;
            }

            public bool Explode()
            {
                foreach (var (number, depth) in Browse())
                {
                    if (depth >= 4 && number.IsPair && number.IsLeaf)
                    {
                        var previousValue = number.Sibling(true);

                        if (previousValue != null)
                        {
                            previousValue.Value += number.Left.Value;
                        }

                        var nextValue = number.Sibling(false);

                        if (nextValue != null)
                        {
                            nextValue.Value += number.Right.Value;
                        }

                        var newValue = new Number { Value = 0, Parent = number.Parent };

                        if (number.Parent!.Left == number)
                        {
                            number.Parent.Left = newValue;
                        }
                        else
                        {
                            number.Parent.Right = newValue;
                        }

                        return true;
                    }
                }

                return false;
            }

            public long Magnitude()
            {
                if (!IsPair)
                {
                    return Value;
                }

                return Left.Magnitude() * 3 + Right.Magnitude() * 2;
            }

            public Number? Sibling(bool left)
            {
                var parent = Parent;
                var current = this;

                while (parent != null)
                {
                    var branch = left ? parent.Right : parent.Left;

                    if (current == branch)
                    {
                        var next = left ? parent.Left : parent.Right;
                        return next!.Browse(!left).First(n => !n.number.IsPair).number;
                    }

                    current = parent;
                    parent = current.Parent;
                }

                return null;
            }

            public IEnumerable<(Number number, int depth)> Browse(bool leftFirst = true, int depth = 0)
            {
                yield return (this, depth);

                if (IsPair)
                {
                    var numbers = leftFirst ? (Left, Right) : (Right, Left);

                    foreach (var item in numbers.Item1.Browse(leftFirst, depth + 1))
                    {
                        yield return item;
                    }

                    foreach (var item in numbers.Item2.Browse(leftFirst, depth + 1))
                    {
                        yield return item;
                    }
                }
            }

            public override string ToString()
            {
                if (IsPair)
                {
                    return $"[{Left},{Right}]";
                }
                else
                {
                    return Value.ToString();
                }
            }

            public Number Clone()
            {
                if (IsPair)
                {
                    var clone = new Number
                    {
                        Left = Left.Clone(),
                        Right = Right.Clone()
                    };

                    clone.Left.Parent = clone;
                    clone.Right.Parent = clone;

                    return clone;
                }
                else
                {
                    return new Number { Value = Value };
                }
            }

            private void Assign(Number value)
            {
                if (Left == null)
                {
                    Left = value;
                }
                else
                {
                    Right = value;
                }
            }
        }

        public override void Solve2()
        {
            var numbers = File.ReadLines(Input).Select(Number.Parse).ToArray();

            long max = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < numbers.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    var magnitude = (numbers[i].Clone() + numbers[j].Clone()).Magnitude();

                    if (magnitude > max)
                    {
                        max = magnitude;
                    }
                }
            }

            Console.WriteLine(max);
        }
    }
}