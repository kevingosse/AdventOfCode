using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal abstract class Problem
    {
        protected Problem()
        {
            Input = Path.Combine(Environment.CurrentDirectory, "Input", GetType().Namespace!.Split('_').Last(), $"{GetType().Name}.txt");
        }

        public int Year => int.Parse(GetType().Namespace!.Split('_').Last());

        public int Day => int.Parse(GetType().Name[3..]);

        protected string Input { get; }

        public virtual void Solve()
        {
        }

        public virtual void Solve2()
        {
        }
    }
}
