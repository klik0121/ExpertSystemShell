using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases;

namespace ExpertSystemShell.Solvers.ProductionModel
{
    public class DepthFirstDirectProductionSolver: DirectProductionSolver
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DirectProductionSolver"/>.
        /// </summary>
        /// <param name="knBase">База знаний.</param>
        public DepthFirstDirectProductionSolver(IKnowledgeBase knBase) : base(knBase)
        {

        }

        /// <summary>
        /// Выбирает одно правило из оставшихся равноправных правил, используя какую-нибудь
        /// эвристику (хоть рандомом).
        /// </summary>
        /// <param name="statements">Список правил в порядке активации.</param>
        /// <returns></returns>
        public override ILogicalStatement ChooseOne(ICollection<ILogicalStatement> statements)
        {
            //т.к. правила находятся в поряке активации, то последнее правило в коллекции
            //даст нам depth first strategy
            var result = statements.Last(); //последнее правило
            return result;
        }
    }
}
