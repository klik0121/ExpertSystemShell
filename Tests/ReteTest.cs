using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpertSystemShell.KnowledgeBases;
using ExpertSystemShell.KnowledgeBases.ProductionModel;
using ExpertSystemShell.Parsers;
using ExpertSystemShell.Expressions;
using ExpertSystemShell.KnowledgeBases.StorageServices;

namespace Tests
{
    [TestClass]
    public class ReteTest
    {
        [TestMethod]
        public void TestReteCreation()
        {
            //это прсото тест на создание
            //корректность создания и работы проверяется в тесте решателя
            
            IParser parser = new PrModelParser(new LogicalExpressionHelper());
            IStorageService service = new PrMInMemoryStService();
            ProductionModelReteNetwork rete = new ProductionModelReteNetwork(service);
            foreach(var statement in parser.ParseRules(Properties.Resources.sampleBase))
            {
                rete.AddStatement(statement);
            }
        }
    }
}
