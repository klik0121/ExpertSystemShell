using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases.StorageServices;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    public class ProdRuleKnBase: AbstractKnowledgeBase
    {

        public ProdRuleKnBase(IStorageService stService): base(stService)
        {
        }

        /// <summary>
        /// Проверяет на конфликк два правила. Возвращает true, если два правила конфликуют.
        /// </summary>
        /// <param name="st1">The ST1.</param>
        /// <param name="st2">The ST2.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override bool CheckConflict(ILogicalStatement st1, ILogicalStatement st2)
        {
            return false;
        }
        /// <summary>
        /// Разрешает логические конфликты в базе.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void RemoveConflicts()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Проверяет истинность логического высказывания.
        /// </summary>
        /// <param name="statement">Логическое утверждение..</param>
        /// <returns>
        /// Возвращает <c>true</c>, если правило можно выполнить.
        /// </returns>
        public override bool CheckStatement(ILogicalStatement statement)
        {
            ProductionRule rule = (ProductionRule)statement;
            foreach(string variableName in rule.Condition.VariableNames)
            {
                foreach (var fact in workMemory)
                    rule.Condition.SetVariable(fact.Name, fact);
            }
            return rule.Condition.Calculate() == true;
        }
    }
}
