using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace MyParallel
{
    public class ParallelForTest
    {
        protected int min;
        protected int max;
        protected int start;
        protected int end;
        protected int avg;

        public ParallelForTest(int min, int max, int start = 0, int end = 100)
        {
            avg = (start + end) / 2;
        }

        public void RunParallel()
        {
            var exceptionQueue = new ConcurrentQueue<Exception>();
            Parallel.For(start, end, (i) =>
                {
                    try
                    {
                        Random rnd = new Random();
                        int xi = rnd.Next(start, end);
                        if (xi > max)
                            throw new MoreThanMaximumException(string.Format("Итерация {0}. {1} > {2}.", i, xi, max));
                        else if (xi < min)
                            throw new LessThanMinimumException(string.Format("Итерация {0}. {1} < {2}.", i, xi, min));
                    }
                    catch(Exception e)
                    {
                        exceptionQueue.Enqueue(e);
                    }
                });
            if (exceptionQueue.Count > 0)
                throw new AggregateException(exceptionQueue);
        }
        public void Run()
        {
            var exceptionQueue = new ConcurrentQueue<Exception>();
            Random rnd = new Random();
            for(int i = start; i < end; i++)
            {
                try
                {                    
                    int xi = rnd.Next(start, end);
                    if (xi > max)
                        throw new MoreThanMaximumException(string.Format("Итерация {0}. {1} > {2}.", i, xi, max));
                    else if (xi < min)
                        throw new LessThanMinimumException(string.Format("Итерация {0}. {1} < {2}.", i, xi, min));
                }
                catch (Exception e)
                {
                    exceptionQueue.Enqueue(e);
                }
            }
            if (exceptionQueue.Count > 0)
                throw new AggregateException(exceptionQueue);
        }
    }

    [Serializable]
    public class LessThanMinimumException : Exception
    {
        public LessThanMinimumException() { }
        public LessThanMinimumException(string message) : base(message) { }
        public LessThanMinimumException(string message, Exception inner) : base(message, inner) { }
        protected LessThanMinimumException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class MoreThanMaximumException : Exception
    {
        public MoreThanMaximumException() { }
        public MoreThanMaximumException(string message) : base(message) { }
        public MoreThanMaximumException(string message, Exception inner) : base(message, inner) { }
        protected MoreThanMaximumException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
