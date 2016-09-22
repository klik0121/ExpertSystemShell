using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases;
using ExpertSystemShell.KnowledgeBases.ProductionModel;

namespace ExpertSystemShell.Solvers
{
    /// <summary>
    /// Интерфейс для солверов для продукционных баз знаний. В качестве алгоритма сравнения 
    /// можно использовать алгоритм Rete. Первый к разработке.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.Solvers.ILogicalSolver" />
    public interface IProductionSolver: ILogicalSolver
    {
        /// <summary>
        /// Выбирает одно правило из оставшихся равноправных правил, используя какую-нибудь 
        /// эвристику (хоть рандомом).
        /// </summary>
        /// <param name="statements">The statements.</param>
        /// <returns></returns>
        ILogicalStatement ChooseOne(ICollection<ILogicalStatement> statements);
    }
}
