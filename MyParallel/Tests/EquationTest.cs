using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyParallel;

namespace Tests
{
    [TestClass]
    public class EquationTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Equation equation = new Equation((x) => { return (x - 1) * (x - 2); });
            double eps = 1E-3;
            double secondRoot = equation.FindRoot(0, 10, eps);
            double root = equation.FindRootParallel(0, 10, eps);            
            Assert.IsTrue(((Math.Abs(root) - 1) <= eps) || ((Math.Abs(root) - 2) <= eps));
        }
    }
}
