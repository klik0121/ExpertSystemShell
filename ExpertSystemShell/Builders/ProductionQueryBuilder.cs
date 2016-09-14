using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases;
using ExpertSystemShell.Solvers;
using ExpertSystemShell.KnowledgeBases.ProductionModel;
using ExpertSystemShell.Parsers.Grammars.ProductionModel;
using ExpertSystemShell.Solvers.ProductionModel;
using Diggins.Jigsaw;

namespace ExpertSystemShell.Builders
{
    /// <summary>
    /// Построитель для логического запроса к продукционной базе знаний.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.Builders.IBuilder{ExpertSystemShell.Solvers.ILogicalQuery}" />
    public class ProductionQueryBuilder: IBuilder<ILogicalQuery>
    {
        protected Dictionary<string, IBuilder<IKnowledgeBaseAction>> actionBuilders;

        /// <summary>
        /// Инициализирует новый экземпляр. <see cref="ProductionQueryBuilder"/>.
        /// </summary>
        public ProductionQueryBuilder()
        {
            actionBuilders = new Dictionary<string, IBuilder<IKnowledgeBaseAction>>();
            actionBuilders.Add(ProductionActionGrammar.AddFact.Name, 
                new AddFactActionBuilder());
        }

        #region IBuilder<ILogicalQuery> Members

        /// <summary>
        /// Возвращает новый экземпляр <see cref="T" />, построенный по заданному AST-дереву.
        /// </summary>
        /// <param name="node">Корень AST-дерева.</param>
        /// <returns>
        /// Возвращает новый экземпляр <see cref="T" />, построенный по заданному AST-дереву.
        /// </returns>
        public ILogicalQuery Build(Diggins.Jigsaw.Node node)
        {
            Node actionList = node[ProductionActionGrammar.ProductionActionList.Name];
            IEnumerable<Node> facts = from n in node.Nodes
                                      where n.Label == ProductionQueryGrammar.FactName.Name
                                      select n;
            List<IKnowledgeBaseAction> actions = new List<IKnowledgeBaseAction>();
            foreach(var n in actionList.Nodes)
                actions.Add(actionBuilders[n.Label].Build(n));
            List<ProductionFact> queriedFacts = new List<ProductionFact>();
            foreach (var n in facts)
                queriedFacts.Add(new ProductionFact(n.Text, null));
            return new ProductionFactQuery(actions, queriedFacts);
        }

        #endregion
    }
}
