using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpertSystemShell.Parsers.Grammars;
using ExpertSystemShell.Parsers.Grammars.ProductionModel;
using ExpertSystemShell.KnowledgeBases.ProductionModel;
using ExpertSystemShell.Builders;
using ExpertSystemShell.Expressions;
using ExpertSystemShell.Parsers;
using ExpertSystemShell.KnowledgeBases;
using ExpertSystemShell.Solvers;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class ProductionParsingTest
    {
        [TestMethod]
        public void TestBuildingAddFactAction()
        {
            IBuilder<IKnowledgeBaseAction> builder = new AddFactActionBuilder();
            string addFact = "'погода - ветренно'";
            AddFactAction addFactAction =
                (AddFactAction)builder.Build(ProductionActionGrammar.AddFact.Parse(addFact)[0]);
            ProductionFact fact = addFactAction.Fact;
            Assert.IsTrue(fact.Name == "погода" && fact.Value == "ветренно");
        }
        [TestMethod]
        public void TestBuildingProductionRule()
        {
            string rule = "на случай дождя : если 'холодно - да' и 'влажность-высокая' то 'будет дождь - да', 'взять зонт - да'";
            LogicalExpressionHelper eh = new LogicalExpressionHelper();
            IBuilder<ILogicalStatement> builder = new ProductionRuleBuilder(eh);
            ProductionRule r = (ProductionRule)builder.Build(
                ProductionRuleGrammar.ProductionRule.Parse(rule)[0]);
            Assert.IsTrue(r.HasName && r.Name == "на случай дождя");
            Assert.IsTrue(r.Condition != null &&
                r.Condition.ToString() == "холодно - да & влажность - высокая");
            Assert.IsTrue(r.Actions.Count() == 2);
        }
        [TestMethod]
        public void TestParsingRules()
        {
            string rules = "на случай дождя : если 'холодно - да' и 'влажность-высокая' то 'будет дождь - да', 'взять зонт - да'";
            IParser parser = new PrModelParser(new LogicalExpressionHelper());
            ILogicalStatement st = parser.ParseRule(rules);
            Assert.IsTrue(st is ProductionRule); //правило построено правильно (см. предыдущий тест)
            rules = rules + ";" + rules + ";" + rules + ";" + rules + ";";
            IEnumerable<ILogicalStatement> statements = parser.ParseRules(rules);
            //правила построены правильно (т.к. отдельные правила строятся правильно)
            //проверим количество правил
            Assert.IsTrue(statements.Count() == 4); 
        }
        [TestMethod]
        public void TestParsingQuery()
        {
            string query = "если 'погода - ветренно', 'дождь - да' то взять зонт=? одеться теплее=?";
            IParser parser = new PrModelParser(new LogicalExpressionHelper());
            ILogicalQuery q = parser.ParseQuery(query);
            Assert.IsTrue(q.GetQueriedItems().Count() == 2);
            Assert.IsTrue(q.GetPreQueryActions().Count() == 2);
            Assert.IsTrue(q.GetPreQueryActions().All((a) => { return a is AddFactAction; }));
            Assert.IsTrue(q.GetQueriedItems().All((a) => { return a is ProductionFact; }));
        }
    }
}
