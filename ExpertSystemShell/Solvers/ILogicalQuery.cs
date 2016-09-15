using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases;

namespace ExpertSystemShell.Solvers
{
    /// <summary>
    /// Интерфейс для логического запроса.
    /// </summary>
    public interface ILogicalQuery
    {
        /// <summary>
        /// Возвращает перечисление, содержащее цели запроса.
        /// </summary>
        /// <returns>Возвращает перечисление, содержащее цели запроса.</returns>
        IEnumerable<IData> GetQueriedItems();
        /// <summary>
        /// Возвращает перечисление, содержащие действия по инициализации запроса.
        /// </summary>
        /// <returns>Возвращает перечисление, содержащие действия по инициализации запроса.</returns>
        IEnumerable<IKnowledgeBaseAction> GetPreQueryActions();
    }
}
