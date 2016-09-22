using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpertSystemShell;
using ExpertSystemShell.Parsers.Grammars.ProductionModel;
using System.Text.RegularExpressions;

namespace Tests
{
    [TestClass]
    public class ProductionGrammarTest
    {
        [TestMethod]
        public void TestFactRecognizing()
        {
            string fact = "'погода - ветренно'";
            Assert.IsTrue(ProductionFactGrammar.ProductionFact.ExactMatch(fact));
        }
        [TestMethod]
        public void TestAddFactAction()
        {
            string action = "'погода - ветренно'";
            Assert.IsTrue(ProductionActionGrammar.ProductionAction.ExactMatch(action));
        }
        [TestMethod]
        public void TestAddMoreFacts()
        {
            string action = "'погода - ветренно', 'ветер - сильный'";
            Assert.IsTrue(ProductionActionGrammar.ProductionActionList.ExactMatch(action));
        }
        [TestMethod]
        public void TestLogicalExpression()
        {
            string fact = "'погода - ветренно' или ('ветренно - да' и 'ветер - сильный')";
            string condition = fact;
            Assert.IsTrue(ProductionExprGrammar.Expression.ExactMatch(condition));
            condition = "( " + fact + ")";
            Assert.IsTrue(ProductionExprGrammar.Expression.ExactMatch(condition));
            condition = condition + " или " + fact;
            Assert.IsTrue(ProductionExprGrammar.Expression.ExactMatch(condition));
            condition = condition + " и (" + condition + ")";
            Assert.IsTrue(ProductionExprGrammar.Expression.ExactMatch(condition));
        }
        [TestMethod]
        public void TestProductionRule()
        {
            string rule = "на случай дождя : если 'холодно - да' то 'будет дождь - да', 'взять зонт - да'";
            Assert.IsTrue(ProductionRuleGrammar.ProductionRule.ExactMatch(rule));
            
        }
        [TestMethod]
        public void TestProductionRuleWithName()
        {
            string rule = " имя из нескольких слов с пробелами : если 'холодно - да' то 'будет дождь - да'";
            Assert.IsTrue(ProductionRuleGrammar.ProductionRule.ExactMatch(rule));
        }
        [TestMethod]
        public void TestProductionRuleList()
        {
            string rule = "если 'холодно - да' то 'будет дождь - да'";
            rule = rule + ";" + rule + " ; " + rule;
            Assert.IsTrue(ProductionRuleGrammar.ProductionRuleList.ExactMatch(rule));
        }
        [TestMethod]
        public void TestFactQuery()
        {
            string query = "если 'погода - ветренно', 'дождь - да' то взять зонт=? одеться теплее=?";
            Assert.IsTrue(ProductionQueryGrammar.Query.ExactMatch(query));
        }
    }
}
