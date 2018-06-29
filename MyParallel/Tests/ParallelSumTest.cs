using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyParallel;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Tests
{
    [TestClass]
    public class ParallelSumTest
    {
        protected double[] arr;
        protected Stopwatch sw;

        [TestInitialize]
        public void InitBigArrayArray()
        {
            Random rnd = new Random();
            arr = new double[10000000];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = rnd.Next(20);
        }
        [TestMethod]
        public void TestPyramid()
        {
            sw = new Stopwatch();
            sw.Start();
            double sum = arr.Sum();
            sw.Stop();
            long sumTime = sw.ElapsedMilliseconds;
            sw.Reset();
            sw.Start();
            double pyrSum = arr.PyramidSum(2);
            sw.Stop();
            long pyrTime = sw.ElapsedMilliseconds;
            Assert.IsTrue(sum == pyrSum);
            Assert.IsTrue(pyrTime < sumTime);
        }
        [TestMethod]
        public void TestSegment()
        {
            sw = new Stopwatch();
            sw.Start();
            double sum = arr.Sum();
            sw.Stop();
            long sumTime = sw.ElapsedMilliseconds;
            sw.Reset();
            sw.Start();
            double segSum = arr.SegmentSum(10);
            sw.Stop();
            long segTime = sw.ElapsedMilliseconds;
            Assert.IsTrue(sum == segSum);
            Assert.IsTrue(segTime < sumTime);
        }
        [TestMethod]
        public void TestStep()
        {
            sw = new Stopwatch();
            sw.Start();
            double sum = arr.Sum();
            sw.Stop();
            long sumTime = sw.ElapsedMilliseconds;
            sw.Reset();
            sw.Start();
            double stepSum = arr.SegmentStepSum(10);
            sw.Stop();
            long stepTime = sw.ElapsedMilliseconds;
            Assert.IsTrue(sum == stepSum);
            Assert.IsTrue(stepTime < sumTime);
        }
    }
}
