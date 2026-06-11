using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService_2.Quartz
{
    public sealed class ExecutingPool
    {
        private static readonly Lazy<ExecutingPool> instance = new Lazy<ExecutingPool>(() => new ExecutingPool(), LazyThreadSafetyMode.ExecutionAndPublication);
        private object lockObj;
        private AutoResetEvent autoEvent;
        private Task task;
        private TimeSpan maxRunningTime;
        private string _maxRunningTime = "01:00:00";

        public string MaxRunningTime { get { return _maxRunningTime; } set { _maxRunningTime = value; } }

        private ExecutingPool()
        {
            Queue = new Queue<IExecuable>();
            lockObj = new object();
            autoEvent = new AutoResetEvent(false);
            maxRunningTime = TimeSpan.Parse(MaxRunningTime);
            task = Task.Run(() => ThreadProc());
        }

        static ExecutingPool() { }

        public static ExecutingPool Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private Queue<IExecuable> Queue { get; set; }

        public void Enqueue(IExecuable job)
        {
            lock (lockObj)
            {
                if (!Queue.Any(q => q.Name == job.Name))
                {
                    Queue.Enqueue(job);
                }

                autoEvent.Set();
            }
        }

        private void ThreadProc()
        {
            while (true)
            {
                while (Queue.Count > 0)
                {
                    IExecuable item = null;

                    lock (lockObj)
                    {
                        item = Queue.Dequeue();
                    }

                    if (item != null)
                    {
                        var tokenSource = new CancellationTokenSource();
                        CancellationToken token = tokenSource.Token;
                        Task.WaitAny(Task.Run(() =>
                        {
                            Thread thread = Thread.CurrentThread;
                            using (token.Register(thread.Abort))
                            {
                                item.Execute();
                            }
                        }, token), Task.Delay(maxRunningTime));

                        tokenSource.Cancel();
                    }
                }

                autoEvent.WaitOne();
            }
        }
    }
}
