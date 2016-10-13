using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases;
using ExpertSystemShell.Solvers;

namespace ExpertSystemShell.Solvers.ProductionModel
{
    /// <summary>
    /// Представляет абстрактный механизм решения для продукционной модели.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.Solvers.IProductionSolver" />
    public abstract class AbstractProductionSolver: IProductionSolver
    {
        protected IKnowledgeBase knBase;
        protected ILogicalQuery cachedQuery;
        protected List<ILogicalResult> cachedResult;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AbstractProductionSolver"/>.
        /// </summary>
        /// <param name="knBase">База знаний.</param>
        public AbstractProductionSolver(IKnowledgeBase knBase)
        {
            this.knBase = knBase;
            this.cachedResult = new List<ILogicalResult>();
        }

        /// <summary>
        /// Получает ответ на логический запрос.
        /// </summary>
        /// <param name="query">Логический запрос.</param>
        /// <returns>Возвращает результат логического запроса.</returns>
        protected abstract ILogicalResult Solve(ILogicalQuery query);

        #region IProductionSolver Members

        /// <summary>
        /// Выбирает одно правило из оставшихся равноправных правил, используя какую-нибудь
        /// эвристику (хоть рандомом).
        /// </summary>
        /// <param name="statements">The statements.</param>
        /// <returns></returns>
        public ILogicalStatement ChooseOne(ICollection<ILogicalStatement> statements)
        {
            ILogicalStatement result = statements.First();
            statements.Remove(result);
            return result;
        }
        /// <summary>
        /// Получает ответ на запроса пользователя.
        /// </summary>
        /// <param name="query">Запрос к логической базе знаний..</param>
        /// <returns>
        /// Возвращает ответ на запрос пользователя.
        /// </returns>
        public ILogicalResult GetResult(ILogicalQuery query)
        {
            if (query.Equals(cachedQuery)) return cachedResult.Last();
            cachedResult = new List<ILogicalResult>();
            Solve(query);
            cachedQuery = query;
            return cachedResult.Last();
        }
        /// <summary>
        /// Получает полный вывод в ответ на запрос пользователя.
        /// </summary>
        /// <param name="query">Запрос к логической базе знаний.</param>
        /// <returns>
        /// Возвращает полный вывод в ответ на запрос пользователя.
        /// </returns>
        public IEnumerable<ILogicalResult> GetConclusion(ILogicalQuery query)
        {
            if (query.Equals(cachedQuery)) return cachedResult;
            cachedResult = new List<ILogicalResult>();
            Solve(query);
            cachedQuery = query;
            return cachedResult;
        }

        #endregion
    }
}
