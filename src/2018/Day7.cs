namespace AdventOfCode._2018;

internal class Day7 : Problem
{
    public override void Solve()
    {
        var steps = new Dictionary<char, Step>();
        foreach (var line in File.ReadLines(Input))
        {
            var (parent, child) = line.Match(@"Step (\w) must be finished before step (\w) can begin").As<char, char>();

            var parentStep = steps.GetOrAdd(parent, c => new(c));
            var childStep = steps.GetOrAdd(child, c => new(c));

            parentStep.Children.Add(childStep);
            childStep.Parents.Add(parentStep);
        }

        var rootStep = new Step(' ')
        {
            Children = steps.Values.Where(s => s.Parents.Count == 0).ToList()
        };

        string path = "";

        var queue = new PriorityQueue<Step, char>();
        queue.Enqueue(rootStep, rootStep.Name);

        while (queue.Count > 0)
        {
            var step = queue.Dequeue();

            if (step.IsDone)
            {
                continue;
            }

            path += step.Name;
            step.IsDone = true;

            foreach (var child in step.Children)
            {
                if (child.AreRequirementsMet())
                {
                    queue.Enqueue(child, child.Name);
                }
            }
        }

        Console.WriteLine(path.TrimStart());
    }

    public override void Solve2()
    {
        const int nbWorkers = 5;
        const int additionalTimePerStep = 60;

        var steps = new Dictionary<char, Step>();
        foreach (var line in File.ReadLines(Input))
        {
            var (parent, child) = line.Match(@"Step (\w) must be finished before step (\w) can begin").As<char, char>();

            var parentStep = steps.GetOrAdd(parent, c => new(c));
            var childStep = steps.GetOrAdd(child, c => new(c));

            parentStep.Children.Add(childStep);
            childStep.Parents.Add(parentStep);
        }

        var queue = new PriorityQueue<Step, char>();

        foreach (var step in steps.Values.Where(s => s.Parents.Count == 0))
        {
            queue.Enqueue(step, step.Name);
        }

        var workers = new (Step? Step, int RemainingTime)[nbWorkers];

        int time;
        int completedSteps = 0;

        for (time = 0; completedSteps < steps.Count; time++)
        {
            for (int i = 0; i < workers.Length; i++)
            {
                ref var worker = ref workers[i];

                if (worker.Step != null)
                {
                    var remainingTime = --worker.RemainingTime;

                    if (remainingTime == 0)
                    {
                        worker.Step.IsDone = true;
                        completedSteps++;

                        foreach (var child in worker.Step.Children)
                        {
                            if (child.AreRequirementsMet())
                            {
                                queue.Enqueue(child, child.Name);
                            }
                        }

                        worker.Step = null;
                    }
                }
            }

            for (int i = 0; i < workers.Length; i++)
            {
                ref var worker = ref workers[i];

                if (worker.Step == null)
                {
                    if (queue.TryDequeue(out var nextStep, out _))
                    {
                        worker.Step = nextStep;
                        worker.RemainingTime = additionalTimePerStep + worker.Step.Name - 'A' + 1;
                    }
                }
            }
        }

        Console.WriteLine(time - 1);
    }

    private class Step(char name)
    {
        public char Name { get; } = name;
        public List<Step> Children { get; init; } = [];
        public List<Step> Parents { get; } = [];
        public bool IsDone { get; set; }

        public bool AreRequirementsMet() => Parents.All(p => p.IsDone);
    }
}
