using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyParallel
{
    public class Equation
    {
        protected Func<double, double> function;
        protected static long maxIterationNum;

        public Equation(Func<double, double> function)
        {
            this.function = function;
            maxIterationNum = 100000000;
        }

        public double FindRoot(double a, double b, double eps)
        {
            long i = 0;
            double bestX = a;
            double bestFX = double.MaxValue;
            while(i < maxIterationNum && Math.Abs(bestFX) > eps)
            {
                double xi = GenerateRandomX(a, b);
                double fxi = function(xi);
                double absFXi = Math.Abs(fxi);
                if (absFXi < bestFX)
                {
                    bestX = xi;
                    bestFX = absFXi;
                }
                i++;
            }
            return bestX;
        }

        public double FindRootParallel(double a, double b, double eps)
        {
            long i = 0;
            double bestX = a;
            object locker = new object();
            Parallel.ForEach(Infinite(), (ingored, loopState) =>
                {
                    double xi = GenerateRandomX(a, b);
                    double fxi = function(xi);
                    double absFXi = Math.Abs(fxi);
                    if(absFXi <= eps) //найден результат
                    {
                        bestX = xi;
                        loopState.Stop();
                    }
                    lock(locker) //увеличиваем количество итераций
                    {
                        i++;
                    }
                    if(i >= maxIterationNum) //превышено количество итераций
                    {
                        loopState.Stop();
                    }
                });
            return bestX;
        }
        private IEnumerable<bool> Infinite()
        {
            while (true) yield return true;
        }

        private IEnumerable<int> Infinite1()
        {
            int i = 0;
            while (i < maxIterationNum)
            {
                yield return i;
                i++;
            }
        }
        private double GenerateRandomX(double a, double b)
        {
            Random rnd = new Random();
            double mult = rnd.NextDouble();
            return a + (b - a) * mult;
        }
    }
}
