using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpertSystemShell.Expressions;
using System.Text.RegularExpressions;

namespace Tests
{
    [TestClass]
    public class LogicalExpressionTest
    {
        ExpressionHelper eh = new LogicalExpressionHelper();

        [TestMethod]
        public void TestSimpleLogicalExpression()
        {
            string expression = "'погода - ветренно'";
           
            Expression exp = eh.CreateExpression(expression);
            exp.SetVariable<string>("погода", "ветренно");
            Assert.IsTrue(exp.Calculate() == true);
        }
        [TestMethod]
        public void TestBinaryOperators()
        {
            string expression = "'погода - ветренно' и 'ветер - сильный'";
            Expression exp = eh.CreateExpression(expression);
            exp.SetVariable<string>("погода", "ветренно");
            exp.SetVariable<string>("ветер", "сильный");
            Assert.IsTrue(exp.Calculate() == true);
        }
        [TestMethod]
        public void TestParanExp()
        {
            string expression = "'погода - ветренно' или ('ветренно - да' и 'ветер - сильный')";
            Expression exp = eh.CreateExpression(expression);
            exp.SetVariable<string>("погода", "ясно");
            exp.SetVariable<string>("ветер", "сильный");
            exp.SetVariable<string>("ветренно", "да");
            Assert.IsTrue(exp.Calculate() == true);
        }
        [TestMethod]
        public void TestFullExp()
        {
            string expression = "Не('погода - ветренно' или ('ветренно - да' и 'ветер - сильный'))";
            Expression exp = eh.CreateExpression(expression);
            exp.SetVariable<string>("погода", "ясно");
            exp.SetVariable<string>("ветер", "сильный");
            exp.SetVariable<string>("ветренно", "да");
            Assert.IsTrue(exp.Calculate() == false);
        }
    }
}
