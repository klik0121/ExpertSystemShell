using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyParallel;

namespace Tests
{
    [TestClass]
    public class SortingTest
    {
        [TestMethod]
        public void TestMergeCorrectness()
        {
            int[] arr = GenerateArr();
            ParallelSorting.MergeSort(arr);
            Assert.IsTrue(ParallelSorting.IsSorted(arr));
        }

        public int[] GenerateArr()
        {
            int[] result = new int[10];
            Random rnd = new Random();
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = rnd.Next(100);
            }
            return result;
        }
        [TestMethod]
        public void TestPMergeCorrectess()
        {
            int[] arr = GenerateArr();
            ParallelSorting.PMergeSort(arr, 10000);
            Assert.IsTrue(ParallelSorting.IsSorted(arr));
        }

        [TestMethod]
        public void TestPMergeSpeed()
        {
            int[] arr = GenerateArr();
            int[] arr1 = new int[arr.Length];
            arr.CopyTo(arr1, 0);
        }
    }
}
