using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.Solvers;
using ExpertSystemShell.KnowledgeBases;
using System.Runtime.Serialization;
using ExpertSystemShell.Parsers;
using ExpertSystemShell.ExplanationSystems;

namespace ExpertSystemShell
{
    /// <summary>
    /// Базовый класс для экспертных систем. Предполагает возможность замены всех компонентов системы 
    /// "на лету". Возможно, нужно будет сделать класс действительным вместо абстрактного.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.IExpertSystem" />
    public class ExpertSystemBase: IExpertSystem
    {
        protected IKnowledgeBase knBase; //содержит заменяемое хранилище и базу
        protected ILogicalSolver solver; //заменяемый "решатель"
        protected IParser parser; //заменяемый парсер запросов
        protected IExplanationSystem explSystem; //заменяемая система объяснений
        protected IEnumerable<ILogicalResult> conclusion; //предыдущий вывод

        public ExpertSystemBase(IKnowledgeBase knBase, ILogicalSolver solver, IParser parser)
        {
            this.knBase = knBase;
            this.solver = solver;
            this.parser = parser;
        }

        #region IExpertSystem Members

        /// <summary>
        /// Получает логический ответ на запрос пользователя.
        /// </summary>
        /// <param name="query">Запрос пользователя.</param>
        /// <returns>
        /// Возвращает логический ответ на запрос пользователя.
        /// </returns>
        public ILogicalResult GetResult(string query)
        {
            conclusion = solver.GetConclusion(parser.ParseQuery(query));
            return conclusion.Last();
        }
        /// <summary>
        /// Получает строковое объяснение предыдущего вывода.
        /// </summary>
        /// <returns>
        /// Возвращает строковое объяснение.
        /// </returns>
        public string GetExplanation()
        {
            return explSystem.GetExplanation(conclusion);
        }
        public void AddRules(string rules)
        {
            foreach(var rule in parser.ParseRules(rules))
            {
                knBase.AddStatement(rule);
            }
        }

        #endregion
    }
}
