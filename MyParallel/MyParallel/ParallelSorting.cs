using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MyParallel
{
    public static class ParallelSorting
    {
        public static void MergeSort(int[] arr)
        {
            MergeSort(arr, 0, arr.Length - 1);
        }
        private static void MergeSort(int[] arr, int l, int r)
        {
            int split;
            if (l < r)
            {
                split = (l + r) / 2;
                MergeSort(arr, l, split);
                MergeSort(arr, split + 1, r);
                Merge(arr, l, split, r);
            }
        }
        private static void Merge(int[] arr, int l, int split, int r)
        {
            int i = l;
            int j = split + 1;
            int k = 0;
            int[] temp = new int[r - l + 1];
            while (i <= split && j <= r)
            {
                if (arr[i] < arr[j])
                {
                    temp[k] = arr[i];
                    i++;
                }
                else
                {
                    temp[k] = arr[j];
                    j++;
                }
                k++;
            }
            while (i <= split)
            {
                temp[k] = arr[i]; i++; k++;
            }
            while (j <= r)
            {
                temp[k] = arr[j]; j++; k++;
            }
            temp.CopyTo(arr, l);
        }
        private static void ParallelMerge(int[] arr, int l, int split, int r)
        {
            if(r - l <= 3)
            {
                Merge(arr, l, r, split);
            }
            else
            {
                Thread left = new Thread(() => 
                {
                    ParallelMerge(arr, l, (l + split) / 2, split);
                });
                Thread right = new Thread(() => 
                {
                    ParallelMerge(arr, (l + split) / 2 + 1,
                        ((l + split) / 2 + 1 + r) / 2, r);
                });
                left.Start();
                right.Start();
                left.Join();
                right.Join();
            }

        }
        public static void PMergeSort(int[] arr, int limit = 10000)
        {
            PMergeSort(arr, 0, arr.Length - 1, limit);
        }
        private static void PMergeSort(int[] arr, int l, int r, int limit)
        {
            int split;
            if (l < r)
            {
                split = (l + r) / 2;
                if (split - l + 1 < limit)
                {
                    MergeSort(arr, l, split);
                    MergeSort(arr, split + 1, r);
                }
                else
                {
                    Thread left = new Thread(() =>
                    {
                        PMergeSort(arr, l, split, limit);
                    });
                    left.Start();
                    Thread right = new Thread(() =>
                    {
                        PMergeSort(arr, split + 1, r, limit);
                    });
                    right.Start();
                    left.Join();
                    right.Join();
                }         
                Merge(arr, l, split, r);
            }
        }
        public static bool IsSorted(int[] arr)
        {
            for(int i = 1; i < arr.Length; i++)
            {
                if (arr[i] < arr[i - 1])
                    return false;
            }
            return true;
        }
    }
}
