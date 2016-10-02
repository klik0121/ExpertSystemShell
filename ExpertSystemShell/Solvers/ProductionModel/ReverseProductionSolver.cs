using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases;
using ExpertSystemShell.KnowledgeBases.ProductionModel;
using ExpertSystemShell.Expressions;

namespace ExpertSystemShell.Solvers.ProductionModel
{
    /// <summary>
    /// Механизм обратного вывода в продукционной модели.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.Solvers.ProductionModel.AbstractProductionSolver" />
    public class ReverseProductionSolver: AbstractProductionSolver
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ReverseProductionSolver"/>.
        /// </summary>
        /// <param name="knBase">База знаний.</param>
        public ReverseProductionSolver(IKnowledgeBase knBase): base(knBase)
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
            List<IData> res = new List<IData>();
            foreach (var target in queriedData)
            {
                if (target.Value == null)
                {
                    foreach (var possibleTarget in GetPossibleTargets(target))
                    {
                        if (GetResultRec(possibleTarget))
                        {
                            //возможная цель реализована
                            res.Add(possibleTarget);
                            break;
                        }
                    }
                }
                else
                {
                    if (GetResultRec(target))
                        res.Add(target);
                }
            }
            ILogicalResult result = new ResultingFactSet(res);
            cachedResult.Add(result);
            return result;
        }

        /// <summary>
        /// Процедура рекурсивного вывода заданной цели.
        /// </summary>
        /// <param name="target">Цель вывода.</param>
        /// <returns>Возвращает <c>true</c>, если вывод удачен.</returns>
        protected bool GetResultRec(IData target)
        {
            if (AlredySolved(target)) return true;
            IEnumerable<ILogicalStatement> set = GetSetContainingTarget(target);
            foreach (var statement in set)
            {
                ProductionRule rule = (ProductionRule)statement;
                if (GetResultRec(rule.Condition))
                {
                    knBase.AddData(target);
                    return true;
                }

            }
            return false;
        }
        /// <summary>
        /// Процедура рекурсивного подтверждения заданного выражения.
        /// </summary>
        /// <param name="expression">Выражение.</param>
        /// <returns>Возвращает <c>true</c>, если выражение удалось подтвердить.</returns>
        protected bool GetResultRec(Expression expression)
        {
            if (expression.IsFact()) return GetResultRec(expression.GetFact());
            else
            {
                Expression left = null;
                Expression right = null;
                if (expression.SplitAnd(out left, out right))
                {
                    return GetResultRec(left) && GetResultRec(right);
                }
                else if (expression.SplitOr(out left, out right))
                {
                    return GetResultRec(left) || GetResultRec(right);
                }
            }
            return false;
        }
        /// <summary>
        /// Возвращает <c>true</c>, если заданная цель уже выведена.
        /// </summary>
        /// <param name="target">Цель вывода..</param>
        /// <returns>Возвращает <c>true</c>, если заданная цель уже выведена.</returns>
        protected bool AlredySolved(IData target)
        {
            return knBase.CurrentData.Any((a) => { return a.Name == target.Name && a.Value == target.Value; });
        }
        /// <summary>
        /// Возвращает множество правил, содержащих в правой части цель вывода.
        /// </summary>
        /// <param name="target">Цель вывода.</param>
        /// <returns>Возвращает множество правил, содержащих в правой части цель вывода.</returns>
        protected IEnumerable<ILogicalStatement> GetSetContainingTarget(IData target)
        {
            return knBase.Where((a) => { return FullyContainsTarget(a, target); });
        }
        /// <summary>
        /// Возвращает <c>true</c>, если заданное правило содержит полностю заданную цель.
        /// </summary>
        /// <param name="statement">Логичесткое утверждение.</param>
        /// <param name="target">Цель вывода.</param>
        /// <returns>Возвращает <c>true</c>, если заданное правило содержит полностю заданную цель.</returns>
        protected bool FullyContainsTarget(ILogicalStatement statement, IData target)
        {
            ProductionRule rule = statement as ProductionRule;
            if (rule != null)
            {
                foreach (var action in rule.Actions)
                {
                    AddFactAction addFact = action as AddFactAction;
                    if (addFact != null && addFact.Fact.Name == target.Name &&
                        addFact.Fact.Value == target.Value)
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Получает возможные цели вывода. Используется в том случае, если в запросе не указано 
        /// значение цели вывода.
        /// </summary>
        /// <param name="statement">Логичесткое утверждение.</param>
        /// <param name="target">Цель вывода.</param>
        /// <returns>Получает возможные цели вывода</returns>
        protected IData GetPossibleTarget(ILogicalStatement statement, IData target)
        {
            ProductionRule rule = statement as ProductionRule;
            if (rule != null)
            {
                foreach (var action in rule.Actions)
                {
                    AddFactAction addFact = action as AddFactAction;
                    if (addFact != null && addFact.Fact.Name == target.Name)
                        return addFact.Fact;
                }
            }
            return null;
        }
        /// <summary>
        /// Получает возможные цели вывода. Используется в том случае, если в запросе не указано 
        /// значение цели вывода.
        /// </summary>
        /// <param name="target">Цель вывода.</param>
        /// <returns>Получает возможные цели вывода</returns>
        protected IEnumerable<IData> GetPossibleTargets(IData target)
        {
            List<IData> result = new List<IData>();
            foreach (var statement in knBase)
            {
                IData newTarget = GetPossibleTarget(statement, target);
                if (newTarget != null)
                    result.Add(newTarget);
            }
            return result;
        }
    }
}
