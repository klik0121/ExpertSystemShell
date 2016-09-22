using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpertSystemShell.Parsers.Grammars;
using ExpertSystemShell.Parsers.Grammars.ProductionModel;
using ExpertSystemShell.KnowledgeBases.ProductionModel;
using ExpertSystemShell.Builders;
using ExpertSystemShell.Expressions;
using ExpertSystemShell.Parsers;
using ExpertSystemShell.KnowledgeBases;
using ExpertSystemShell.Solvers.ProductionModel;
using ExpertSystemShell.Solvers;
using ExpertSystemShell.KnowledgeBases.StorageServices;
using ExpertSystemShell;
using System.IO;

namespace Tests
{
    [TestClass]
    public class ProductionSolversTest
    {
        IExpertSystem expert;
        [TestMethod]
        public void TestDirectSolver()
        {
            string query = "если 'валютный курс доллара - падает' то уровень цен на бирже=?";
            ILogicalResult result = expert.GetResult(query);
            Assert.IsTrue(result is ResultingFactSet);
            Assert.IsTrue(result.ToString() == "'уровень цен на бирже - падает'");
        }
        [TestInitialize]
        public void Initialize()
        {
            IKnowledgeBase knBase = new ProdRuleKnBase(new PrMInMemoryStService());
            ILogicalSolver solver = new DirectProductionSolver(knBase);
            LogicalExpressionHelper eh = new LogicalExpressionHelper();
            IParser parser = new PrModelParser(eh);
            expert = new ExpertSystemBase(knBase, solver, parser);
            string rules = File.ReadAllText("base.txt");
            expert.AddRules(rules);
        }
    }
}
