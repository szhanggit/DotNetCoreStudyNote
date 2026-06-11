using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService_2.Quartz
{
    public sealed class SingleExecutingPool : IDisposable
    {
        private static readonly Lazy<SingleExecutingPool> instance = new Lazy<SingleExecutingPool>(() => new SingleExecutingPool(), LazyThreadSafetyMode.ExecutionAndPublication);
        private object lockObj;
        private AutoResetEvent autoEvent;
        private Task task;
        private bool disposed = false;

        private SingleExecutingPool()
        {
            Queue = new Queue<IExecuable>();
            lockObj = new object();
            autoEvent = new AutoResetEvent(false);
            task = Task.Run(() => ThreadProc());
        }

        static SingleExecutingPool() { }

        public static SingleExecutingPool Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private Queue<IExecuable> Queue { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Enqueue(IExecuable job, bool ignoreExist = false)
        {
            lock (lockObj)
            {
                if (ignoreExist)
                {
                    Queue.Enqueue(job);
                }
                else
                {
                    if (!Queue.Any(q => q.Name == job.Name))
                    {
                        Queue.Enqueue(job);
                    }
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
                        try
                        {
                            item.Execute();
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

                autoEvent.WaitOne();
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                autoEvent.Dispose();
            }

            disposed = true;
        }
    }
}
