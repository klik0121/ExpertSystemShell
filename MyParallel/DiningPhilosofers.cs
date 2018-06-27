using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MyParallel
{
    public class DiningPhilosofers
    {
        protected int philosofers;
        protected int count;
        protected SemaphoreSlim[] forks;
        protected volatile bool stop = false;

        protected const int thiskTime = 100;
        protected const int waitTime = 50;
        protected const int eatTime = 20;

        protected Thread[] threads;
        protected string[] states;

        public string[] States
        {
            get { return states; }
        }

        public DiningPhilosofers(int philosofers, int count)
        {
            this.philosofers = philosofers;
            this.count = count;
            states = new string[philosofers];
            this.threads = new Thread[philosofers];
            forks = new SemaphoreSlim[philosofers];
            for(int i = 0; i < philosofers; i++)
            {
                states[i] = "";
                forks[i] = new SemaphoreSlim(1, 1);
            }
        }
        public void StartDinner()
        {
            stop = false;
            for(int i = 0; i < philosofers; i++)
            {
                int index = i;
                threads[index] = new Thread(StartDining);
                threads[index].Start(index);
            }
        }
        public void StartDining(object num)
        {
            int i = (int)num;
            SemaphoreSlim left = forks[i];
            SemaphoreSlim right = forks[(i + 1) % philosofers];
            if(i % 2 == 1)
            {
                SemaphoreSlim temp = left;
                left = right;
                right = left;
            }
            for(int j = 0; j < count && !stop; j++)
            {
                states[i] += "thinks => ";
                Thread.Sleep(thiskTime);
                left.Wait();
                states[i] += "waits => ";
                if (stop) return;
                Thread.Sleep(waitTime);
                right.Wait();
                if (stop) return;
                states[i] += "eats => ";                
                Thread.Sleep(eatTime);
                left.Release();
                right.Release();
            }
        }
        public void StopDinner()
        {
            stop = true;
        }
    }
}
