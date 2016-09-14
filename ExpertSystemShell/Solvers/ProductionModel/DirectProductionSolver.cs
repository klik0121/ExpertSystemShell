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
    public class DirectProductionSolver: IProductionSolver
    {
        protected IKnowledgeBase knBase;
        protected ILogicalQuery cachedQuery;
        protected List<ILogicalResult> cachedResult;

        public DirectProductionSolver(IKnowledgeBase knBase)
        {
            this.knBase = knBase;
            cachedResult = new List<ILogicalResult>();
        }

        #region IProductionSolver Members

        /// <summary>
        /// Выполняет правую часть продукционного правила.
        /// </summary>
        /// <param name="statement">The statement.</param>
        public void ExecuteStatement(KnowledgeBases.ILogicalStatement statement)
        {
            statement.Execute(knBase);
        }

        /// <summary>
        /// Выбирает одно правило из оставшихся равноправных правил, используя какую-нибудь
        /// эвристику.
        /// </summary>
        /// <param name="statements">The statements.</param>
        /// <returns></returns>
        public KnowledgeBases.ILogicalStatement ChooseOne(IEnumerable<KnowledgeBases.ILogicalStatement> statements)
        {
            return statements.First();
        }

        #endregion

        #region ILogicalSolver Members

        /// <summary>
        /// Получает ответ на запроса пользователя.
        /// </summary>
        /// <param name="query">Запрос к логической базе знаний..</param>
        /// <returns>
        /// Возвращает ответ на запрос пользователя.
        /// </returns>
        public ILogicalResult GetResult(ILogicalQuery query)
        {
            this.cachedQuery = query;
            cachedResult.Clear();
            IEnumerable<IKnowledgeBaseAction> init = query.GetPreQueryAction();
            foreach(var item in init)
                item.Execute(knBase);
            List<IData> queriedData = query.GetQueriedItems().ToList();
            List<ILogicalStatement> executedStatements = new List<ILogicalStatement>();
            IEnumerable<ILogicalStatement> ready = from ILogicalStatement r in knBase
                                                   where
                                                       !executedStatements.Contains(r) &&
                                                       knBase.CheckStatement(r)
                                                   select r;
            while (ready.Count() > 0 &&
                queriedData.Count((a) => { return a.Value == null; }) > 0) 
            {
                ILogicalStatement st = ChooseOne(ready);
                st.Execute(knBase);
                executedStatements.Add(st);
                foreach(IData data in knBase.CurrentData)
                {
                    foreach(IData qData in queriedData)
                    {
                        if(qData.Value == null && qData.Name == data.Name)
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
            GetResult(query);
            return cachedResult;
        }

        #endregion
    }
}
