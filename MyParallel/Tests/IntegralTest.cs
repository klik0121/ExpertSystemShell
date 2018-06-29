using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyParallel;

namespace Tests
{
    [TestClass]
    public class IntegralTest
    {
        private double eps = 1E-5;


        [TestMethod]
        public void TestSimpleIntegral()
        {
            Func<double, double> function = (x) => { return x * x; };
            double integralResult = function.Integral1(0, 10, eps);
            Assert.IsTrue(Math.Abs(integralResult - 1000 / (double)3) <= eps);
        }
        [TestMethod]
        public void TestSimplePIntegral()
        {
            Func<double, double> function = (x) => { return x * x; };
            double integralResult = function.PIntegral1(0, 10, eps, 10);
            Assert.IsTrue(Math.Abs(integralResult - 1000 / (double)3) <= eps);
        }

        [TestMethod]
        public void TestPiCalculation()
        {
            Func<double, double> function = (x) => { return 1 / (1 + x * x); };
            double integral = function.PIntegral1(0, 1, eps);
            Assert.IsTrue(Math.Abs(Math.PI - 4 * integral) < eps * 10);
        }
    }
}
