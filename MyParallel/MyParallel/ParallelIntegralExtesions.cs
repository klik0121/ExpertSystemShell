using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MyParallel
{
    public static class ParallelIntegralExtesions
    {

        public static double Integral1(this Func<double, double> function,
            double a, double b, double eps)
        {
            if (b < a) return -function.Integral1(a, b, eps);
            double n = 2;
            double prev = 0;
            double result = 0;
            do
            {
                prev = result;
                double x = a;
                double l = (b - a) / n;
                result = 0;
                for (int i = 0; i <= n; i++)
                {
                    result += function(x);
                    x += l;
                }
                n = n * 2;
                result *= l;
            } while (Math.Abs(result - prev) >= eps);
            return result;
        }

        public static double PIntegral1(this Func<double, double> function, double a,
            double b, double eps, int count = 4)
        {
            Thread[] threads = new Thread[count];
            double[] results = new double[count];
            double lenght = (b - a) / count;
            b = a + lenght;
            for(int i = 0; i < count; i++)
            {
                threads[i] = new Thread((x) =>
                   {
                       object[] args = (object[])x;
                       results[(int)args[0]] = function.Integral1((double)args[1], (double)args[2],
                            (double)args[3]);
                   });
                threads[i].Start(new object[] { i, a, b, eps / count });
                a = b;
                b += lenght;
            }
            foreach (var item in threads)
                item.Join();
            return results.Sum();
        }
    }
}
