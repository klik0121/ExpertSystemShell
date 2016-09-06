using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diggins.Jigsaw;
using ExpertSystemShell.KnowledgeBases;

namespace ExpertSystemShell.Builders
{
    /// <summary>
    /// Интерфейс построителя для <see cref="ExpertSystemShell.KnowledgeBases.ILogicalStatement"/>.
    /// </summary>
    public interface IStatementBuilder
    {
        /// <summary>
        /// По заданному дереву строит экземпляр <see cref="ExpertSystemShell.KnowledgeBases.ILogicalStatement"/>.
        /// </summary>
        /// <param name="node">Корень дерева разбора.</param>
        /// <returns>Возвращает построенный экземпляр <see cref="ExpertSystemShell.KnowledgeBases.ILogicalStatement"/>.</returns>
        ILogicalStatement Build(Node node);
    }
}
