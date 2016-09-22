using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemShell.Solvers
{
    /// <summary>
    /// Интерфейс для подсистемы решения.
    /// </summary>
    public interface ILogicalSolver
    {
        /// <summary>
        /// Получает ответ на запроса пользователя.
        /// </summary>
        /// <param name="query">Запрос к логической базе знаний..</param>
        /// <returns>Возвращает ответ на запрос пользователя.</returns>
        ILogicalResult GetResult(ILogicalQuery query);
        /// <summary>
        /// Получает полный вывод в ответ на запрос пользователя.
        /// </summary>
        /// <param name="query">Запрос к логической базе знаний.</param>
        /// <returns>Возвращает полный вывод в ответ на запрос пользователя.</returns>
        IEnumerable<ILogicalResult> GetConclusion(ILogicalQuery query);
    }
}
