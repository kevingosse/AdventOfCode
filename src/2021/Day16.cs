using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    internal class Day16 : Problem
    {
        public override void Solve()
        {
            var input = ToBinary(File.ReadAllText(Input));

            var reader = new StringReader(input);

            var packets = new Queue<Packet>();

            packets.Enqueue(Packet.ReadPacket(reader, out _));

            long result = 0;

            while (packets.Count > 0)
            {
                var packet = packets.Dequeue();

                result += packet.Version;

                foreach (var subPacket in packet.SubPackets)
                {
                    packets.Enqueue(subPacket);
                }
            }

            Console.WriteLine(result);
        }

        public override void Solve2()
        {
            var input = ToBinary(File.ReadAllText(Input));

            var reader = new StringReader(input);

            var packet = Packet.ReadPacket(reader, out _);

            Console.WriteLine(packet.Evaluate());
        }

        private static byte ToByte(char c)
        {
            return c switch
            {
                '0' => 0x0,
                '1' => 0x1,
                '2' => 0x2,
                '3' => 0x3,
                '4' => 0x4,
                '5' => 0x5,
                '6' => 0x6,
                '7' => 0x7,
                '8' => 0x8,
                '9' => 0x9,
                'A' => 0xA,
                'B' => 0xB,
                'C' => 0xC,
                'D' => 0xD,
                'E' => 0xE,
                'F' => 0xF,
                _ => throw new InvalidOperationException()
            };
        }

        private static long ReadValue(StringReader stream, int length)
        {
            long result = 0;

            for (int i = length - 1; i >= 0; i--)
            {
                var nextBit = (char)stream.Read();

                if (nextBit == '0')
                {
                    continue;
                }

                result |= 1L << i;
            }

            return result;
        }

        private static long ReadValue(StringBuilder str)
        {
            long result = 0;

            for (int i = str.Length - 1; i >= 0; i--)
            {
                var nextBit = str[str.Length - i - 1];

                if (nextBit == '0')
                {
                    continue;
                }

                result |= 1L << i;
            }

            return result;
        }

        private static string ToBinary(string input)
        {
            var result = new StringBuilder(input.Length * 8);

            foreach (var c in input.TrimEnd('\n'))
            {
                var value = ToByte(c);
                result.Append(Convert.ToString(value, 2).PadLeft(4, '0'));
            }

            return result.ToString();
        }

        [DebuggerDisplay("Version: {Version}, Id: {Id}, Value: {Value}")]
        private class Packet
        {
            internal static Packet ReadPacket(StringReader reader, out int size)
            {
                var version = ReadValue(reader, 3);
                var id = ReadValue(reader, 3);

                size = 6;

                if (id == 4)
                {
                    var packet = ReadLiteral(reader, version, out var literalSize);

                    size += literalSize;

                    return packet;
                }
                else
                {
                    var packet = ReadOperator(reader, version, id, out var operatorSize);

                    size += operatorSize;

                    return packet;
                }
            }

            private static Packet ReadOperator(StringReader reader, long version, long id, out int size)
            {
                var lengthType = (char)reader.Read();

                size = 1;

                var packet = new Packet { Version = version, Id = id };

                if (lengthType == '0')
                {
                    var length = ReadValue(reader, 15);

                    size += 15 + (int)length;

                    while (length > 0)
                    {
                        packet.SubPackets.Add(ReadPacket(reader, out var subPacketSize));
                        length -= subPacketSize;
                    }

                }
                else
                {
                    var numberOfPackets = ReadValue(reader, 11);

                    size += 11;

                    for (int i = 0; i < numberOfPackets; i++)
                    {
                        packet.SubPackets.Add(ReadPacket(reader, out var subPacketSize));
                        size += subPacketSize;
                    }
                }

                return packet;
            }

            private static Packet ReadLiteral(StringReader reader, long version, out int size)
            {
                var binaryNumber = new StringBuilder();

                bool lastChunk = false;

                size = 0;

                while (!lastChunk)
                {
                    size += 5;

                    var bit = (char)reader.Read();

                    if (bit == '0')
                    {
                        lastChunk = true;
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        binaryNumber.Append((char)reader.Read());
                    }
                }

                return new Packet
                {
                    Id = 4,
                    Version = version,
                    Value = ReadValue(binaryNumber)
                };
            }

            public long Id { get; private set; }

            public long Value { get; private set; }

            public long Version { get; private set; }

            public List<Packet> SubPackets { get; } = new();

            public long Evaluate()
            {
                return Id switch
                {
                    0 => SubPackets.Select(p => p.Evaluate()).Sum(),
                    1 => SubPackets.Select(p => p.Evaluate()).Aggregate(1L, (p1, p2) => p1 * p2),
                    2 => SubPackets.Select(p => p.Evaluate()).Min(),
                    3 => SubPackets.Select(p => p.Evaluate()).Max(),
                    4 => Value,
                    5 => SubPackets[0].Evaluate() > SubPackets[1].Evaluate() ? 1 : 0,
                    6 => SubPackets[0].Evaluate() < SubPackets[1].Evaluate() ? 1 : 0,
                    7 => SubPackets[0].Evaluate() == SubPackets[1].Evaluate() ? 1 : 0,
                    _ => throw new InvalidOperationException("Unexpected id: " + Id),
                };
            }
        }
    }
}