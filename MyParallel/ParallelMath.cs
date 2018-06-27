using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MyParallel
{
    public static class ParallelMath
    {
        public static double PSin(double x, double eps = 1E-5, int count = 4)
        {
            x = x % (Math.PI * 2); //учитываем периодичность
            int sign = Math.Sign(x);
            sign *= -Math.Sign(x - Math.PI);
            x %= Math.PI;
            double actualX = Math.Abs(x);
            double si = actualX;
            double x2 = actualX * actualX;
            Thread[] threads = new Thread[count];
            double[] result = new double[count];
            for(int i = 0; i < count; i++)
            {
                //number - номер потока (i)
                threads[i] = new Thread((args) =>
                {
                    object[] arr = (object[])args;
                    int j = (int)arr[0];
                    double startValue = (double)arr[1];
                    result[j] = PSin(x, j, eps, count, startValue);
                });
                threads[i].Start(new object[] { i, si});
                int i2 = (i + 1) * 2;
                si *= -(x2) / (i2 * (i2 + 1));
            }
            foreach (var item in threads)
                item.Join();
            return result.Sum() * sign;
        }
        private static double PSin(double x, int i, double eps, int p, double startValue)
        {
            double si = startValue;
            double sum = startValue;
            int sign = p % 2 == 0? 1 : -1; //если количество потоков чётно, то знак 1, иначе -1
            double xp = Math.Pow(x, 2 * p); //x в степени удвоенного количества потоков
            i++;
            while (Math.Abs(si) >= eps) 
            {
                si *= sign * xp;
                double div = 1;
                for (int j = 0; j < p; j++)
                {
                    double i2 = 2 * i;
                    div *= (i2) * (i2 + 1);
                    i++;
                }
                si /= div;
                sum += si;
            } 
            return sum;
        }
        public static double Sin(double x, double eps = 1E-5)
        {
            x = x % (Math.PI * 2); //учитываем периодичность
            int sign = Math.Sign(x);
            sign *= -Math.Sign(x - Math.PI);
            x %= Math.PI;
            double actualX = Math.Abs(x);
            double si = actualX;
            double sum = actualX;
            double x2 = actualX * actualX;
            
            int i = 0;
            do
            {
                i++;
                double i2 = 2 * i;
                double div = i2 * (i2 + 1);
                si *= -x2 / div;
                sum += si;
                
            } while (Math.Abs(si) >= eps);
            return sum * sign;
        }

        public static double ASin(double x, double eps = 1E-5)
        {
            double x2 = x * x;
            int i = 0;
            double si = x;
            double k = 0;
            double sum = 0;
            while(Math.Abs(si) > eps)
            {
                sum += si;
                k = 2 * i + 1;
                si *= k * k * x2 / ((k + 1) * (k + 2));
                i++;
            }
            return sum;
        }

        public static double PASin(double x, int count = 4, double eps = 1E-5)
        {
            Thread[] threads = new Thread[count];
            double[] result = new double[count];
            double x2 = x * x;
            double si = x;
            double i = 0;
            for (int j = 0; j < count; j++ )
            {
                threads[j] = new Thread((args) => 
                {
                    object[] arr = (object[])args;
                    int l = (int)arr[0]; //l -номер потока (j)
                    double stValue = (double)arr[1]; //стартовое значение для текщуего потока
                    result[l] = PASin(x, l, count, eps, stValue);
                });
                threads[j].Start(new object[] {j, si});                
                double k = 2 * i + 1;
                si *= k * k * x2 / ((k + 1) * (k + 2));
                i++;

            }
            foreach (var thread in threads)
                thread.Join();
            return result.Sum();
        }

        private static double PASin(double x, int j, int p, double eps, double startValue)
        {
            double si = startValue;
            int i = j;
            double x2p = Math.Pow(x, 2 * p);
            double sum = 0;
            while (Math.Abs(si) >= eps)
            {
                sum += si;
                double pr = 1;
                for (int k = 1; k <= p; k++)
                {
                    pr *= (1 - 1 / (2 * (i + k)));
                }
                si *= pr * x2p * (2 * i + 1) / (2 * (i + p) + 1);
                i += p;
            }
            return sum;
        }
    }
}
