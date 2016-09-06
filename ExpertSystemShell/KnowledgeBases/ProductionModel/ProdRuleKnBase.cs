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
        protected List<ProductionFact> workMem;


        public void AddFact(ProductionFact fact)
        {
            workMem.Add(fact);
        }
        public ProdRuleKnBase(IStorageService stService): base(stService)
        {
            this.workMem = new List<ProductionFact>();
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
            throw new NotImplementedException();
        }
        /// <summary>
        /// Разрешает логические конфликты в базе.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void RemoveConflicts()
        {
            throw new NotImplementedException();
        }
    }
}
