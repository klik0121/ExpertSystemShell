using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MyParallel
{
    public static class ParallelSumExtensions
    {
        /// <summary>
        /// Пирамидальный алгоритм суммы для массива.
        /// </summary>
        /// <param name="arr">Массив элементов.</param>
        /// <param name='limit'>Длина массива, достигнув которой,
        /// вычисления станут последовательными.</param>
        /// <returns>Возвращает сумму элементов массива.</returns>
        public static double PyramidSum(this double[] arr, long limit = 100000)
        {
            return PyramidSubFunc(arr, 0, arr.Length - 1, limit);
        }
        /// <summary>
        /// Вспомогательная функция для пирмаидального алгоритма.
        /// </summary>
        /// <param name="arr">Массив.</param>
        /// <param name="start">Начальный элемент.</param>
        /// <param name="end">Конечный элемент.</param>
        /// <param name='limit'>Длина массива, достигнув которой,
        /// вычисления станут последовательными.</param>
        /// <returns>Возвращает сумму элементов массива, начиная с первого и заканчивая последним
        /// заданным элементом.</returns>
        private static double PyramidSubFunc(double[] arr, int start, int end, long limit)
        {
            if (start - end + 1 <= limit) return SegmentSubFunction(arr, start, end);
            int lenght = (end - start + 1) / 2;
            double l = 0;
            double r = 0;
            Thread left = new Thread(() =>
                { l = PyramidSubFunc(arr, start, start + lenght, limit); });
            Thread right = new Thread(() =>
                { r = PyramidSubFunc(arr, start + lenght + 1, end, limit); });
            left.Start();
            right.Start();
            left.Join();
            right.Join();
            return l + r;
                
        }
        /// <summary>
        /// Сегментный алгоритм нахождения суммы массива.
        /// </summary>
        /// <param name="arr">Массив.</param>
        /// <param name="count">Количество сегментов.</param>
        /// <returns>Возвращает сумму элементов массива.</returns>
        public static double SegmentSum(this double[] arr, int count)
        {
            double[] results = new double[count];
            Thread[] threads = new Thread[count];
            int segmentLenght = arr.Length / count - 1;
            int rem = arr.Length % count;
            int start  = -1;
            int end = -1;
            for(int i = 0; i < count; i++)
            {
                start = ++end;
                end = start + segmentLenght;
                if(rem > 0)
                {
                    rem--;
                    end++;
                }
                threads[i] = new Thread((obj) =>
                    { object[] args = (object[])obj;
                      results[(int)args[1]] = SegmentSubFunction((double[])args[0],
                          (int)args[2], (int)args[3]); });
                threads[i].Start(new object[] {arr, i, start, end});
            }
            foreach (var thread in threads)
                thread.Join();
            return results.Sum();
        }
        /// <summary>
        /// Вспомогтальная функция для сегментного алгоритма.
        /// </summary>
        /// <param name="arr">Массив элементов</param>
        /// <param name="start">Начальный элемент.</param>
        /// <param name="end">Конечный элемент.</param>
        /// <returns>Возвращает сумму элементов массива, начиная с первого и заканчивая последним
        /// заданным элементом.</returns>
        private static double SegmentSubFunction(double[] arr, int start, int end)
        {
            double sum = 0;
            for(int i = start; i <= end; i++)
            {
                sum += arr[i]; 
            }
            return sum;
        }
        /// <summary>
        /// Шаговый алгоритм нахождения суммы массива.
        /// </summary>
        /// <param name="arr">Массив.</param>
        /// <param name="step">Шаг.</param>
        /// <returns>Возвращает суму элементов массива.</returns>
        public static double SegmentStepSum(this double[] arr, int step)
        {
            double[] results = new double[step];
            Thread[] threads = new Thread[step];
            for(int i = 0; i < step; i++)
            {
                threads[i] = new Thread((a) =>
                    { object[] args = (object[])a;
                        results[(int)args[1]] = SegmentStepSubFunc((double[])args[0], (int)args[1],
                        (int)args[2]);
                    });
                threads[i].Start(new object[] { arr, i, step });
            }
            foreach (Thread t in threads)
                t.Join();
            return results.Sum();
        }
        /// <summary>
        /// Вспомогтальеная функция для шагового алгорима.
        /// </summary>
        /// <param name="arr">Массив.</param>
        /// <param name="start">Начальный элемент.</param>
        /// <param name="step">Шаг.</param>
        /// <returns>Возвращает сумму элементов массива, начиная с заданного элемента с заданным 
        /// шагом.</returns>
        private static double SegmentStepSubFunc(double[] arr, int start, int step)
        {
            int i = start;
            double sum = 0;
            while(i < arr.Length)
            {
                sum += arr[i];
                i += step;
            }
            return sum;
        }
    }
}
