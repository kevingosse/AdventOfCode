namespace AdventOfCode._2016
{
    internal class Day8 : Problem
    {
        public override void Solve()
        {
            var display = new char[50, 6];

            foreach (var line in File.ReadLines(Input))
            {
                UpdateScreen(display, line);
            }

            Console.WriteLine(display.OfType<char>().Count(p => p == '#'));
        }

        public override void Solve2()
        {
            var display = new char[50, 6];
            
            foreach (var line in File.ReadLines(Input))
            {
                UpdateScreen(display, line);
            }

            for (int i = 0; i < display.GetLength(1); i++)
            {
                for (int j = 0; j < display.GetLength(0); j++)
                {
                    Console.Write(display[j, i] == '\0' ? ' ' : display[j, i]);
                }

                Console.WriteLine();
            }
        }

        private void UpdateScreen(char[,] display, string command)
        {
            var parts = command.Split(' ');

            if (parts[0] == "rect")
            {
                var (width, height) = parts[1].Split('x').Select(int.Parse);

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        display[x, y] = '#';
                    }
                }

                return;
            }

            if (parts[1] == "row")
            {
                int displayWidth = display.GetLength(0);

                var row = int.Parse(parts[2].Split('=').Last());
                var count = displayWidth - int.Parse(parts[4]);

                for (int iteration = 0; iteration < count; iteration++)
                {
                    var first = display[0, row];

                    for (int i = 0; i < displayWidth - 1; i++)
                    {
                        display[i, row] = display[i + 1, row];
                    }

                    display[displayWidth - 1, row] = first;
                }
            }
            else
            {
                int displayHeight = display.GetLength(1);

                var column = int.Parse(parts[2].Split('=').Last());
                var count = displayHeight - int.Parse(parts[4]);

                for (int iteration = 0; iteration < count; iteration++)
                {
                    var first = display[column, 0];

                    for (int i = 0; i < displayHeight - 1; i++)
                    {
                        display[column, i] = display[column, i + 1];
                    }

                    display[column, displayHeight - 1] = first;
                }
            }
        }
    }
}
