namespace Interactive_Broker.Helpers
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class Parallel
    {
        readonly List<Task> tmp = new List<Task>();
        Task[] tasks;
        public Parallel()
        {
            Invoke(() => { }, () => { });
            Wait();
        }

        public void Wait()
        {
            if (tasks != null)
            {
                Task.WaitAll(tasks);
                tasks = null;
            }
        }

        public bool IsBusy
        {
            get
            {
                if (tasks != null)
                {
                    bool result = Task.WaitAll(tasks, 0);
                    if (result)
                    {
                        tasks = null;
                        return false;
                    }
                    return true;
                }
                return false;
            }
        }

        public bool Invoke(params Action[] actions)
        {
            if (IsBusy) return false;

            tmp.Clear();
            foreach (var action in actions) tmp.Add(new Task(action));
            tasks = tmp.ToArray();
            for (int i = 0; i < tasks.Length; i++)
                tasks[i].Start();

            return true;
        }
    }
}
