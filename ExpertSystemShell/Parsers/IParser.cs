using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases;
using ExpertSystemShell.Solvers;

namespace ExpertSystemShell.Parsers
{
    /// <summary>
    /// Интерфейс для парсера.
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Разбирает запрос пользователя и формирует множество логических параметров запроса.
        /// </summary>
        /// <param name="query">Строка-запрос.</param>
        /// <returns>Возвращает перечисление параметров запроса.</returns>
        ILogicalQuery ParseQuery(string query);
        /// <summary>
        /// Разбирает логическое правило.
        /// </summary>
        /// <param name="rule">Строка-правило.</param>
        /// <returns>Возвращает созданный экземпляр правила.</returns>
        ILogicalStatement ParseRule(string rule);
        /// <summary>
        /// Разбирает список правил.
        /// </summary>
        /// <param name="rules">Строка, содержащая список правил.</param>
        /// <returns>Возвращает перечисление, содержащее разобранные правила.</returns>
        IEnumerable<ILogicalStatement> ParseRules(string rules);
    }
}
