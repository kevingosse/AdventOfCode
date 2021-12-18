using Newtonsoft.Json.Linq;
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
    internal class Day12 : Problem
    {
        public override void Solve()
        {
            var input = File.ReadAllText(Input);

            var matches = Regex.Matches(input, "-?[0-9]+");

            var result = matches.Select(m => int.Parse(m.Value)).Sum();

            Console.WriteLine(result);
        }

        public override void Solve2()
        {
            var input = File.ReadAllText(Input);

            var json = JObject.Parse(input);

            var total = 0;

            foreach (var prop in json.Properties())
            {
                total += Compute(prop.Value) ?? 0;
            }

            Console.WriteLine(total);
        }

        private int? Compute(JToken value)
        {
            int sum = 0;

            if (value is JArray array)
            {
                foreach (var item in array)
                {
                    sum += Compute(item) ?? 0;
                }
            }
            else if (value is JObject obj)
            {
                foreach (var property in obj.Properties())
                {
                    var val = Compute(property.Value);

                    if (val == null)
                    {
                        return 0;
                    }

                    sum += val.Value;
                }
            }
            else if (value is JValue jvalue)
            {
                if (jvalue.Value is long)
                {
                    return (int)(long)jvalue.Value;
                }
                else if (jvalue.Value is string)
                {
                    return (string)jvalue.Value == "red" ? (int?)null : 0;
                }
            }

            return sum;
        }
    }
}