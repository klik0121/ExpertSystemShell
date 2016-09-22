using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.Parsers.Grammars.ProductionModel;
using ExpertSystemShell.KnowledgeBases.ProductionModel;
using Diggins.Jigsaw;
using ExpertSystemShell.KnowledgeBases;

namespace ExpertSystemShell.Builders
{
    /// <summary>
    /// Построитель для <see cref="ExpertSystemShell.KnowledgeBases.ProductionModel.AddFactAction"/>.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.Builders.IProductionActionBuilder" />
    public class AddFactActionBuilder: IBuilder<IKnowledgeBaseAction>
    {
        #region IProductionActionBuilder Members

        /// <summary>
        /// По заданному дереву строит <see cref="ExpertSystemShell.KnowledgeBases.ProductionModel.IKnowledgeBaseAction" />.
        /// </summary>
        /// <param name="node">Корень дерева разбора..</param>
        /// <returns>
        /// Возвращает построенный экземпляр <see cref="ExpertSystemShell.KnowledgeBases.ProductionModel.IKnowledgeBaseAction" />.
        /// </returns>
        public KnowledgeBases.IKnowledgeBaseAction Build(Diggins.Jigsaw.Node node)
        {
            Node fact = node[0];
            string name = fact[ProductionFactGrammar.Property.Name].Text;
            string value = fact[ProductionFactGrammar.Value.Name].Text;
            return new AddFactAction(new ProductionFact(name, value));
        }

        #endregion
    }
}
