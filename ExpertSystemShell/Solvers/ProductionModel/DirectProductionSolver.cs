using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.Solvers;
using ExpertSystemShell.KnowledgeBases;
using ExpertSystemShell.KnowledgeBases.ProductionModel;

namespace ExpertSystemShell.Solvers.ProductionModel
{
    /// <summary>
    /// Механизм прямого вывода в продукционной модели.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.Solvers.ProductionModel.AbstractProductionSolver" />
    public class DirectProductionSolver: AbstractProductionSolver
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DirectProductionSolver"/>.
        /// </summary>
        /// <param name="knBase">База знаний.</param>
        public DirectProductionSolver(IKnowledgeBase knBase): base(knBase)
        {
        }

        /// <summary>
        /// Добавляет новые правила к списку уже готовых к выполнению.
        /// </summary>
        /// <param name="ready">Список готовых правил.</param>
        protected void AddRecentRules(List<ILogicalStatement> ready)
        {
            if (knBase.StateChanged)
            {
                foreach (ILogicalStatement statement in knBase)
                {
                    if (!ready.Contains(statement) && knBase.CheckStatement(statement))
                        ready.Add(statement);
                }
            }
        }
        /// <summary>
        /// Получает ответ на логический запрос.
        /// </summary>
        /// <param name="query">Логический запрос.</param>
        /// <returns>
        /// Возвращает результат логического запроса.
        /// </returns>
        protected override ILogicalResult Solve(ILogicalQuery query)
        {
            knBase.ClearWorkMemory();
            this.cachedQuery = query;
            cachedResult.Clear();
            IEnumerable<IKnowledgeBaseAction> init = query.GetPreQueryActions();
            foreach (var item in init)
                item.Execute(knBase);
            List<IData> queriedData = query.GetQueriedItems().ToList();
            List<ILogicalStatement> ready = new List<ILogicalStatement>();
            AddRecentRules(ready);
            while (ready.Count > 0 && queriedData.Count((a) => { return a.Value == null; }) > 0)
            {
                ILogicalStatement st = ChooseOne(ready);
                st.Execute(knBase);
                AddRecentRules(ready);
                foreach (IData data in knBase.CurrentData)
                {
                    foreach (IData qData in queriedData)
                    {
                        if (qData.Value == null && qData.Name == data.Name)
                        {
                            qData.Value = data.Value;
                        }
                    }
                }
            }
            ILogicalResult result = new ResultingFactSet(queriedData);
            cachedResult.Add(result);
            return result;
        }
    }
}
