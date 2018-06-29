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
            ready = knBase.ActiveSet.ToList();
            while (ready.Count > 0 && queriedData.Count((a) => { return a.Value == null; }) > 0)
            {
                ILogicalStatement st = ChooseOne(ready);
                ready.Remove(st);
                st.Execute(knBase);
                if(knBase.StateChanged)
                {
                    ready = knBase.ActiveSet.ToList();
                }
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
