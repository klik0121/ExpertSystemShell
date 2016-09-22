using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.Solvers;
using ExpertSystemShell.KnowledgeBases;

namespace ExpertSystemShell
{
    /// <summary>
    /// Предполагаемый интерфейс экспертной ситсемы.
    /// </summary>
    public interface IExpertSystem
    {
        /// <summary>
        /// Получает логический ответ на запрос пользователя.
        /// </summary>
        /// <param name="query">Запрос пользователя.</param>
        /// <returns>Возвращает логический ответ на запрос пользователя.</returns>
        ILogicalResult GetResult(string query);
        /// <summary>
        /// Получает строковое объяснение предыдущего вывода.
        /// </summary>
        /// <returns>Возвращает строковое объяснение.</returns>
        string GetExplanation();
        void AddRules(string rules);
    }
}
