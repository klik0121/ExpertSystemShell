using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace Airport
{
    class Program
    {
        static void Main(string[] args)
        {
            IStorageService service = new PrMInMemoryStService();
            IKnowledgeBase knBase = new ProductionModelReteNetwork(service);
            IProductionSolver solver = new DirectProductionSolver(knBase);
            LogicalExpressionHelper eh = new LogicalExpressionHelper();
            IParser parser = new PrModelParser(eh);
            IExpertSystem expert = new ExpertSystemBase(knBase, solver, parser);
            string rules = Properties.Resources.TravelBase;
            Console.WriteLine("Parsing rules...");
            expert.AddRules(rules);
            Console.WriteLine("Done!");
            string query = "если 'выбор направления-Европа', 'выбор страны-Испания', 'приоритет отеля-центр города', 'приоритет города-экскурсии', 'количество звезд-4' то 'отель=?'";
            //string query = "если 'желание-купить', 'денег-достаточно', 'расстояние-равно', " +
            //    "'цены лучше-РИО' то 'пойти в РИО=?', 'пойти в Рубин=?'";
            Console.WriteLine("Submitting query: {0}", query);
            Console.Write("Result:");
            ResultingFactSet result = (ResultingFactSet)expert.GetResult(query);
            foreach (var item in result)
            {
                if (item.Value != null)
                    Console.WriteLine(item);
            }
            Console.ReadKey();
        }
    }
}
