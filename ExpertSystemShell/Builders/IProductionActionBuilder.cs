using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases.ProductionModel;
using Diggins.Jigsaw;

namespace ExpertSystemShell.Builders
{
    /// <summary>
    /// Интерфейс построителя для <see cref="ExpertSystemShell.KnowledgeBases.ProductionModel.IProductionAction"/>.
    /// </summary>
    public interface IProductionActionBuilder
    {
        /// <summary>
        /// По заданному дереву строит <see cref="ExpertSystemShell.KnowledgeBases.ProductionModel.IProductionAction"/>.
        /// </summary>
        /// <param name="node">Корень дерева разбора..</param>
        /// <returns>Возвращает построенный экземпляр <see cref="ExpertSystemShell.KnowledgeBases.ProductionModel.IProductionAction"/>.</returns>
        IProductionAction Build(Node node);
    }
}
