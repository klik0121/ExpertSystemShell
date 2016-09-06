using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpertSystemShell.Parsers.Grammars;
using ExpertSystemShell.Parsers.Grammars.ProductionModel;
using ExpertSystemShell.KnowledgeBases.ProductionModel;
using ExpertSystemShell.Builders;
using ExpertSystemShell.Expressions;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class ProductionParsingTest
    {
        [TestMethod]
        public void TestBuildingAddFactAction()
        {
            IProductionActionBuilder builder = new AddFactActionBuilder();
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
            IStatementBuilder builder = new ProductionRuleBuilder(eh);
            ProductionRule r = (ProductionRule)builder.Build(
                ProductionRuleGrammar.ProductionRule.Parse(rule)[0]);
            Assert.IsTrue(r.HasName && r.Name == "на случай дождя");
            Assert.IsTrue(r.Condition != null &&
                r.Condition.ToString() == "холодно - да & влажность - высокая");
            Assert.IsTrue(r.Actions.Count() == 2);
        }
    }
}
