using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases;
using ExpertSystemShell.KnowledgeBases.ProductionModel;
using ExpertSystemShell.Expressions;
using Diggins.Jigsaw;
using ExpertSystemShell.Parsers.Grammars.ProductionModel;

namespace ExpertSystemShell.Builders
{
    /// <summary>
    /// Построитель для <see cref="ExpertSystemShell.KnowledgeBases.ProductionModel.ProductionRule"/>.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.Builders.IStatementBuilder" />
    public class ProductionRuleBuilder: IBuilder<ILogicalStatement>
    {
        protected ExpressionHelper eh;
        protected Dictionary<string, IBuilder<IKnowledgeBaseAction>> builders;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ProductionRuleBuilder"/>.
        /// </summary>
        /// <param name="eh">Построитель выражений.</param>
        public ProductionRuleBuilder(ExpressionHelper eh)
        {
            this.eh = eh;
            this.builders = new Dictionary<string, IBuilder<IKnowledgeBaseAction>>();
            builders.Add(ProductionActionGrammar.AddFact.Name, new AddFactActionBuilder());
        }

        #region IStatementBuilder Members

        /// <summary>
        /// По заданному дереву строит экземпляр <see cref="ExpertSystemShell.KnowledgeBases.ILogicalStatement" />.
        /// </summary>
        /// <param name="node">Корень дерева разбора.</param>
        /// <returns>
        /// Возвращает построенный экземпляр <see cref="ExpertSystemShell.KnowledgeBases.ILogicalStatement" />.
        /// </returns>
        public KnowledgeBases.ILogicalStatement Build(Diggins.Jigsaw.Node node)
        {
            Node condition = node[ProductionExprGrammar.Expression.Name];
            Node name = node.Nodes.FirstOrDefault((a) =>
                { return a.Label == ProductionRuleGrammar.Name.Name; });
            Node actionList = node.Nodes.FirstOrDefault((a) =>
                { return a.Label == ProductionActionGrammar.ProductionActionList.Name; });
            Expression expression = eh.CreateExpression(condition);
            string ruleName = name == null ? null : name.Text;
            List<IKnowledgeBaseAction> actions = new List<IKnowledgeBaseAction>();
            foreach (var n in actionList.Nodes)
                actions.Add(builders[n.Label].Build(n));
            return new ProductionRule(ruleName, expression, actions);
        }

        #endregion
    }
}
