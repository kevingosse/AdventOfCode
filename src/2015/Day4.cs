using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    internal class Day4 : Problem
    {
        public override void Solve()
        {
            var key = "yzbqklnj";

            int index = 0;

            var md5 = MD5.Create();

            while (true)
            {
                var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(key + index));

                if (hash[0] == 0x0 && hash[1] == 0x0 && hash[2] <= 0xF)
                {
                    Console.WriteLine(index);
                    return;
                }

                index++;
            }
        }

        public override void Solve2()
        {
            var key = "yzbqklnj";

            int index = 0;

            var md5 = MD5.Create();

            while (true)
            {
                var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(key + index));

                if (hash[0] == 0x0 && hash[1] == 0x0 && hash[2] == 0x0)
                {
                    Console.WriteLine(index);
                    return;
                }

                index++;
            }

        }
    }
}