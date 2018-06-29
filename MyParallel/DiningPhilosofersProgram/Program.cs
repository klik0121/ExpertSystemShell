using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyParallel;
using System.Threading;

namespace DiningPhilosofersProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            DiningPhilosofers dining = new DiningPhilosofers(5, 5);
            const int dinnerTime = 1700;
            dining.StartDinner();
            Thread.Sleep(dinnerTime);
            dining.StopDinner();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(string.Format("philosofer {0}: {1}", i + 1, dining.States[i]));
            }
            Console.ReadKey();
        }
    }
}
