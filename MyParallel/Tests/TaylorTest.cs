using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyParallel;
using System.Diagnostics;

namespace Tests
{
    [TestClass]
    public class TaylorTest
    {
        [TestMethod]
        public void TestSinCorrectness()
        {
            for(int i = 1; i < 100; i++)
            {
                double mySin = ParallelMath.Sin(i, 1E-9);
                double mathSin = Math.Sin(i);
                Assert.IsTrue(Math.Abs(mySin - mathSin) <= 1E-9);
            }
        }
        [TestMethod]
        public void TestPSinCorrectness()
        {
            for (int i = 1; i < 100; i++)
            {
                double mySin = ParallelMath.PSin(i, 1E-9, 2);
                double mathSin = Math.Sin(i);
                Assert.IsTrue(Math.Abs(mySin - mathSin) <= 1E-9);
            }
        }

        [TestMethod]
        public void TestPSinSpeeed()
        {
            Stopwatch sw = new Stopwatch();
            Stopwatch pSw = new Stopwatch();
            double res;

            sw.Start();
            for(int i = 1; i < 100; i++)
            {
                res = ParallelMath.Sin(i, 1E-20);
            }
            sw.Stop();

            pSw.Start();
            for (int i = 1; i < 100; i++)
            {
                res = ParallelMath.PSin(i, 1E-20, 4);
            }
            pSw.Stop();

            Assert.IsTrue(pSw.ElapsedTicks < sw.ElapsedTicks);
        }

        [TestMethod]
        public void TestASinCorrectnress()
        {
            double x = Math.PI / 15;
            double eps = 1E-9;
            double mathASin = Math.Asin(x);
            double myAsin = ParallelMath.ASin(x, eps);
            Assert.IsTrue(Math.Abs(myAsin - mathASin) <= eps);
        }
        [TestMethod]
        public void TestPAsinCorrectness()
        {
            double x = Math.PI / 15;
            double eps = 1E-9;
            double seqASin = ParallelMath.ASin(x);
            double pAsin = ParallelMath.PASin(x, 1, eps);
            Assert.IsTrue(Math.Abs(pAsin - seqASin) <= eps * 10);
        }
    }
}
